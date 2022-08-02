using AutoMapper;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Errors;
using BuyandRentHomeWebAPI.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            ApiError apiError = new ApiError();
            if(loginRequest.UserName == null || loginRequest.Password == null)
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "Provide mandatory informations";
                apiError.ErrorDetails = "This error appears when Username or Password is missing in request.";
                return BadRequest(apiError);
            }

            var user = await _unitOfWork.UserRepository.Authenticate(loginRequest.UserName, loginRequest.Password);
            if(user == null)
            {
                apiError.ErrorCode = Unauthorized().StatusCode;
                apiError.ErrorMessage = "Invalid User ID or Password";
                apiError.ErrorDetails = "This error appears when provided user id or password doesnot exist.";
                return Unauthorized(apiError);
            }

            var loginResponse = new LoginResponseDto();
            loginResponse.UserName = user.Username;
            loginResponse.Token = createJWT(user);
            return Ok(loginResponse);
        }

        // api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            ApiError apiError = new ApiError();

            if (string.IsNullOrEmpty(register.UserName) || string.IsNullOrEmpty(register.Email) || string.IsNullOrEmpty(register.Password))
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "Provide mandatory informations";
                apiError.ErrorDetails = "This error appears when necessary information is missing in request.";
                return BadRequest(apiError);
            }

            if (await _unitOfWork.UserRepository.UserAlreadyExists(register.UserName))
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User already exists, please try something else";
                apiError.ErrorDetails = "This error appears when username already exist in record.";
                return BadRequest(apiError);
            }

            _unitOfWork.UserRepository.Register(register.UserName, register.Email, register.Password, register.Mobile);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);
        }

            private string createJWT(User user)
        {
            var secretKey = _configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

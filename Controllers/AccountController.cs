using AutoMapper;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var user = await _unitOfWork.UserRepository.Authenticate(loginRequest.Username, loginRequest.Password);
            if(user == null)
            {
                return Unauthorized();
            }
            var loginResponse = new LoginResponseDto();
            loginResponse.Username = user.Username;
            loginResponse.Token = createJWT(user);
            return Ok(loginResponse);
        }

        private string createJWT(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("shhh.. this is my top secret"));

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

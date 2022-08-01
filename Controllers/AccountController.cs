﻿using AutoMapper;
using BuyandRentHomeWebAPI.Dtos;
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
            if(loginRequest.UserName == null || loginRequest.Password == null)
            {
                return BadRequest();
            }

            var user = await _unitOfWork.UserRepository.Authenticate(loginRequest.UserName, loginRequest.Password);
            if(user == null)
            {
                return Unauthorized("Invalid User ID or Password");
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
            if (register.UserName == null || register.Email == null || register.Password == null)
                return BadRequest("Please provide mandatory informations");

            if (await _unitOfWork.UserRepository.UserAlreadyExists(register.UserName))
                return BadRequest("User already exists, please try something else");

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

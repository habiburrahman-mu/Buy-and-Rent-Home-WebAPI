using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BuyandRentHomeWebAPI.Data.Entities;

namespace BuyandRentHomeWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;
        }

        public async Task<User> Authenticate(LoginRequestDto loginRequest)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserName(loginRequest.UserName);
            if (user == null || user.PasswordKey == null)
                return null;

            if (!MatchPasswordHash(loginRequest.Password, user.Password, user.PasswordKey))
                return null;

            return user;
        }

        public LoginResponseDto CreateLoginCredintials(User user)
        {
            var loginResponse = new LoginResponseDto();
            loginResponse.UserName = user.Username;
            loginResponse.Token = createJWT(user);
            return loginResponse;
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await _unitOfWork.UserRepository.UserAlreadyExists(userName);
        }

        public async Task<bool> Register(RegisterDto register)
        {
            _unitOfWork.UserRepository.Register(register.UserName, register.Email, register.Password, register.Mobile);
            return await _unitOfWork.SaveAsync();
        }

        

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordText));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }
                return true;
            }
        }

        private string createJWT(User user)
        {
            var secretKey = _configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Test")
            };
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                //Expires = DateTime.UtcNow.AddSeconds(20),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly BuyRentHomeDbContext _dataContext;

        public UserRepository(BuyRentHomeDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == userName);
            return user;
        }

        public void Register(string userName, string email, string password, string mobile)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            User user = new User();
            user.Username = userName.Trim();
            user.Email = email;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            user.Mobile = mobile;

            _dataContext.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await _dataContext.Users.AnyAsync(x => x.Username == userName.Trim());
        }
    }
}

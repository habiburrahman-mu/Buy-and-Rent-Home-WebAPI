using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using BuyandRentHomeWebAPI.Dtos;
using System.Linq.Expressions;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly BuyRentHomeDbContext _dataContext;

        public UserRepository(BuyRentHomeDbContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<PageResult<User>> GetUserPaginateList(
            int pageNo = 1, int pageSize = 10,
            Expression<Func<User, bool>> filter = null,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null
            )
        {
            IQueryable<User> query = _dataContext.Users;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            query = query.Include(x => x.UserPrivileges).ThenInclude(x => x.Role);

            int count = await query.CountAsync();

            int totalPages = (int)(count > 0 ? Math.Ceiling((double)count / pageSize) : 0);
            var skip = pageSize * (pageNo - 1);
            query = query.Skip(skip).Take(pageSize);

            var resultList = await query.ToListAsync();

            var pageResult = new PageResult<User>
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalRecords = count,
                TotalPages = totalPages,
                ResultList = resultList
            };

            return pageResult;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _dataContext.Users
                .Include(x => x.UserPrivileges).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.Username == userName);
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

using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Common.Constants;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<PageResult<UserDto>> GetUserPaginatedList(PaginationParameter paginationParameter)
        {
            Expression<Func<User, bool>>? filter = null;

            if(!String.IsNullOrEmpty(paginationParameter.SearchingText))
            {
                filter = u => u.Username.ToLower().Contains(paginationParameter.SearchingText.ToLower());
            }

            var paginatedList = await _unitOfWork.UserRepository.GetUserPaginateList(
                paginationParameter.CurrentPageNo, paginationParameter.PageSize, filter);

            var userList = mapper.Map<List<UserDto>>(paginatedList.ResultList);

            var paginatedResult = new PageResult<UserDto>
            {
                PageNo = paginatedList.PageNo,
                PageSize = paginatedList.PageSize,
                TotalPages = paginatedList.TotalPages,
                TotalRecords = paginatedList.TotalRecords,
                ResultList = userList
            };
            return paginatedResult;
        }

        public async Task<User?> GetUserDetail(LoginRequestDto loginRequest)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserName(loginRequest.UserName);
            if (user == null || user.PasswordKey == null)
                return null;

            if (!MatchPasswordHash(loginRequest.Password, user.Password, user.PasswordKey))
                return null;

            return user;
        }

        public async Task<bool> IsUserAlreadyExists(string userName)
        {
            return await _unitOfWork.UserRepository.UserAlreadyExists(userName);
        }

        public async Task<bool> Register(RegisterDto register)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
            }

            User user = new();
            user.Name = register.Name;
            user.Username = register.UserName.Trim();
            user.Email = register.Email;
            user.Mobile = register.Mobile;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;

            UserPrivilege userPrivilege = new();
            userPrivilege.User = user;
            userPrivilege.RoleId = (int)UserRoleIds.User;

            await _unitOfWork.UserPrivilegeRepository.Insert(userPrivilege);

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
    }
}

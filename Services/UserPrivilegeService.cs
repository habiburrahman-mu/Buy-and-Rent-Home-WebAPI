using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Services
{
    public class UserPrivilegeService : IUserPrivilegeService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserPrivilegeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> SaveUserPrivilege(UserPrivilegeSaveDto userPrivilegeSaveDto)
        {
            var userPrivilegeListFromDb = await unitOfWork.UserPrivilegeRepository.GetAll(x => x.UserId == userPrivilegeSaveDto.UserId);

            var createList = new List<UserPrivilege>();
            var deleteList = new List<UserPrivilege>();

            createList = userPrivilegeSaveDto.UserPrivilegeList.Where(x => userPrivilegeListFromDb.FirstOrDefault(y => y.RoleId == x.RoleId) == null).ToList();
            deleteList = userPrivilegeListFromDb.Where(x => userPrivilegeSaveDto.UserPrivilegeList.FirstOrDefault(y => y.RoleId == x.RoleId) == null).ToList();

            await unitOfWork.UserPrivilegeRepository.InsertRange(createList);
            unitOfWork.UserPrivilegeRepository.DeleteRange(deleteList);

            return await unitOfWork.SaveAsync();
        }
    }
}

using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<PageResult<Role>> GetRolePaginatedList(PaginationParameter paginationParameter)
        {
            var paginatedRoleListResult = await unitOfWork.RoleRepository.GetPaginateList(
                paginationParameter.CurrentPageNo, paginationParameter.PageSize);

            var paginatedResult = new PageResult<Role>
            {
                PageNo = paginatedRoleListResult.PageNo,
                PageSize = paginatedRoleListResult.PageSize,
                TotalPages = paginatedRoleListResult.TotalPages,
                TotalRecords = paginatedRoleListResult.TotalRecords,
                ResultList = paginatedRoleListResult.ResultList
            };
            return paginatedRoleListResult;
        }
    }
}

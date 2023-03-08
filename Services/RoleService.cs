using AutoMapper;
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
        private readonly IMapper mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<PageResult<RoleDto>> GetRolePaginatedList(PaginationParameter paginationParameter)
        {
            var paginatedRoleListResult = await unitOfWork.RoleRepository.GetPaginateList(
                paginationParameter.CurrentPageNo, paginationParameter.PageSize);

            var roleList = mapper.Map<List<RoleDto>>(paginatedRoleListResult.ResultList);

            var paginatedResult = new PageResult<RoleDto>
            {
                PageNo = paginatedRoleListResult.PageNo,
                PageSize = paginatedRoleListResult.PageSize,
                TotalPages = paginatedRoleListResult.TotalPages,
                TotalRecords = paginatedRoleListResult.TotalRecords,
                ResultList = roleList
            };
            return paginatedResult;
        }
    }
}
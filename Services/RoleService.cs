using AutoMapper;
using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Services
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

        public async Task<List<RoleDto>> GetRoleList()
        {
            var roleList = await unitOfWork.RoleRepository.GetAll();

            var roleDtoList = mapper.Map<List<RoleDto>>(roleList);

            return roleDtoList;
        }
    }
}
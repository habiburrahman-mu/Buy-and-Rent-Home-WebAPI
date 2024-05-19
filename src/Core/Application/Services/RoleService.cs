using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces.Data;

namespace Application.Services;

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
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;

namespace Application.Services;

public class UserPrivilegeService(IUnitOfWork unitOfWork, IMapper mapper) : IUserPrivilegeService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<bool> SaveUserPrivilege(UserPrivilegeSaveDto userPrivilegeSaveDto)
    {
        var userPrivilegeListFromDb = await unitOfWork.UserPrivilegeRepository.GetAll(x => x.UserId == userPrivilegeSaveDto.UserId);

        var createList = new List<UserPrivilege>();
        var deleteList = new List<UserPrivilege>();

        var mappedUserPrivilegeList = mapper.Map<List<UserPrivilege>>(userPrivilegeSaveDto.UserPrivilegeList);

        createList = mappedUserPrivilegeList.Where(x => userPrivilegeListFromDb.FirstOrDefault(y => y.RoleId == x.RoleId) == null).ToList();
        deleteList = userPrivilegeListFromDb.Where(x => userPrivilegeSaveDto.UserPrivilegeList.FirstOrDefault(y => y.RoleId == x.RoleId) == null).ToList();

        await unitOfWork.UserPrivilegeRepository.InsertRange(createList);
        unitOfWork.UserPrivilegeRepository.DeleteRange(deleteList);

        return await unitOfWork.SaveAsync();
    }
}

using Application.DTOs;

namespace Application.Interfaces;

public interface IRoleService
{
    Task<List<RoleDto>> GetRoleList();
}

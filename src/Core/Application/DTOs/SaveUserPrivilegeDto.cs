using Domain.Entities;

namespace Application.DTOs;

public class UserPrivilegeSaveDto
{
    public int UserId { get; set; }
    public List<UserPrivilegeDto> UserPrivilegeList { get; set; } = null!;
}

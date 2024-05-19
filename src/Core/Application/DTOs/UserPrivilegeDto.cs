namespace Application.DTOs;

public partial class UserPrivilegeDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string? RoleName { get; set; }
}

namespace Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public List<UserPrivilegeDto> UserPrivileges { get; set; }
}

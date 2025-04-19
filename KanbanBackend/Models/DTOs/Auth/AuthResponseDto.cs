using KanbanBackend.Models.DTOs.Users;
namespace KanbanBackend.Models.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; }
    public UserDto User { get; set; }
}
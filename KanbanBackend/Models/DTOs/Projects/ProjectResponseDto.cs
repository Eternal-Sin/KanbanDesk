using KanbanBackend.Models.DTOs.Users;

namespace KanbanBackend.Models.DTOs.Projects;

public class ProjectResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserDto Creator { get; set; }
}
namespace KanbanBackend.Models.DTOs.UserProjects;

public class UserProjectDto
{
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public string UserRole { get; set; } // "Creator", "Member" и т.д.
}
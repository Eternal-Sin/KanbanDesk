namespace KanbanBackend.Models.DTOs.Projects;

public class ProjectCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public required int CreatorId { get; set; }
}
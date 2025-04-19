using KanbanBackend.Models.DTOs.Users;

namespace KanbanBackend.Models.DTOs.Tasks;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserDto? Assignee { get; set; }
    public int ColumnId { get; set; }
}
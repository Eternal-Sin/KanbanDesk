namespace KanbanBackend.Models.DTOs.Tasks
{
    public class TaskUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ColumnId { get; set; }
        public int? ManagerId { get; set; }
    }
}
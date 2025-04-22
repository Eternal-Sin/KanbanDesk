namespace KanbanBackend.Models.DTOs.Tasks
{
    public class TaskCreateDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int ColumnId { get; set; }
        public int CreatorId { get; set; }
        public int? ManagerId { get; set; }
    }
}
namespace KanbanBackend.Models.DTOs.TaskLogs
{
    public class TaskLogDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedAction { get; set; }
    }
}
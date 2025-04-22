using KanbanBackend.Models.DTOs.Users;

namespace KanbanBackend.Models.DTOs.Tasks
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ColumnId { get; set; }
        public UserDto Creator { get; set; }
        public UserDto? Manager { get; set; }
    }
}
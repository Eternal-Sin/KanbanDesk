using KanbanBackend.Models.DTOs.Users;

namespace KanbanBackend.Models.DTOs.UserProjects
{
    public class UserProjectDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int ProjectId { get; set; }
    }
}
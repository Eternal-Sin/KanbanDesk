namespace KanbanBackend.Models
{
    public class UserProject
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public bool IsCreator { get; set; }
        public User User { get; set; }
        public DeskProject Project { get; set; }
    }
}

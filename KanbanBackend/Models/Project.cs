using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; } // Перенесено из UserProject (было IsCreator)
        public DateTime CreatedDate { get; set; }
        public User Creator { get; set; }  // Навигация к User
        public ICollection<Column> Columns { get; set; }
        public ICollection<UserProject> UserProjects { get; set; }
    }
}


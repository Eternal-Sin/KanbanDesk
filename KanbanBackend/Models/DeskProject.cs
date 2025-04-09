using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class DeskProject
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public User Creator { get; set; }
        public ICollection<UserProject> UserProjects { get; set; }
        public ICollection<Column> Columns { get; set; }
    }
}


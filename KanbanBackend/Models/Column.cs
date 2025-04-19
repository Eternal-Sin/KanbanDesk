using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class Column
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; } 
        public Project Project { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}


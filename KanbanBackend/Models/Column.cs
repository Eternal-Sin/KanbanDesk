using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class Column
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public DeskProject Project { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}


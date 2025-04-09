using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class Column
    {
        public int id { get; set; }

        public int idProject { get; set; }

        public DeskProject project { get; set; }

        public string name { get; set; }

        public int order { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}


using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class DeskProject
    {
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public ICollection<Column> Columns { get; set; }
    }
}


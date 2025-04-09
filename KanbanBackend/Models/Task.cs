using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class Task
    {
        public int id { get; set; }

        public int idColumn { get; set; }

        public Column column { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string status { get; set; }

        public DateTime createAt { get; set; }

        public DateTime updateAt { get; set; }

        public ICollection<TaskLog> Logs { get; set; }
    }
}


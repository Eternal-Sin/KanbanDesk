using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class TaskLog
    {
        public int id { get; set; }

        public int idTask { get; set; }

        public Task task { get; set; }

        public string action { get; set; }

        public DateTime createAt { get; set; }
    }
}


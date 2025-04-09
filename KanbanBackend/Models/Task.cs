using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public int TaskManagerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Column Column { get; set; }
        public User TaskManager { get; set; }
        public ICollection<TaskLog> TaskLogs { get; set; }
    }
}


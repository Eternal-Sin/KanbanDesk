using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public int CreatorId { get; set; } 
        public int? ManagerId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Навигационные свойства
        public Column Column { get; set; }
        public User Creator { get; set; }
        public User Manager { get; set; }
        public ICollection<TaskLog> TaskLogs { get; set; }
    }
}


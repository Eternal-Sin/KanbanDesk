using System;
using System.Collections.Generic;

namespace KanbanBackend.Models
{
    public class TaskLog
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedAction { get; set; }
        public Task Task { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Data;

namespace KanbanBackend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }
        public ICollection<Task> ManagedTasks { get; set; }
        public ICollection<Project> CreatedProjects { get; set; } // Добавляем
        public ICollection<Task> CreatedTasks { get; set; } // Добавляем
    }
}


using Microsoft.EntityFrameworkCore;
using KanbanBackend.Models;

namespace KanbanBackend.Data
{
    public class KanbanContext : DbContext
    {
        public DbSet<DeskProject> DeskProjects { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<KanbanBackend.Models.Task> Tasks { get; set; }
        public DbSet<TaskLog> TaskLogs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        public KanbanContext(DbContextOptions<KanbanContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=KanbanDb;User Id=sa;Password=<12345677654321Uu>;;TrustServerCertificate=True;");
            }
        }
    }
}
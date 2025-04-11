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
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=KanbanDb;User Id=sa;Password=12345677654321Uu;TrustServerCertificate=True;");
            }
        }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User -> Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // DeskProject -> User (Creator)
            modelBuilder.Entity<DeskProject>()
                .HasOne(dp => dp.Creator)
                .WithMany() 
                .HasForeignKey(dp => dp.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // UserProject -> User
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.Id) 
                .OnDelete(DeleteBehavior.Cascade);

            // UserProject -> DeskProject
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Column -> DeskProject
            modelBuilder.Entity<Column>()
                .HasOne(c => c.Project)
                .WithMany(p => p.Columns)
                .HasForeignKey(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Task -> Column
            modelBuilder.Entity<KanbanBackend.Models.Task>()
                .HasOne(t => t.Column)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.ColumnId)
                .OnDelete(DeleteBehavior.Cascade);

            // Task -> User (TaskManager)
            modelBuilder.Entity<KanbanBackend.Models.Task>()
                .HasOne(t => t.TaskManager)
                .WithMany(u => u.ManagedTasks)
                .HasForeignKey(t => t.TaskManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // TaskLog -> Task
            modelBuilder.Entity<TaskLog>()
                .HasOne(tl => tl.Task)
                .WithMany(t => t.TaskLogs)
                .HasForeignKey(tl => tl.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
}
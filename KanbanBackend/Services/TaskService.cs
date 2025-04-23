using KanbanBackend.Data;
using KanbanBackend.Models;
using KanbanBackend.Models.DTOs.Tasks;
using KanbanBackend.Models.DTOs.Users;
using Microsoft.EntityFrameworkCore;
using Task = KanbanBackend.Models.Task; 

namespace KanbanBackend.Services
{
    public class TaskService
    {
        private readonly KanbanContext _context;
        private readonly TaskLogService _taskLogService;
        

        public TaskService(KanbanContext context, TaskLogService taskLogService)
        {
            _context = context;
            _taskLogService = taskLogService;
        }

        public async System.Threading.Tasks.Task<TaskResponseDto> CreateTaskAsync(TaskCreateDto dto)
        {
            var task = new Task
            {
                Name = dto.Name,
                Description = dto.Description,
                ColumnId = dto.ColumnId,
                CreatorId = dto.CreatorId,
                ManagerId = dto.ManagerId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            await LoadRelatedData(task);
            return MapToDto(task);
        }

        public async System.Threading.Tasks.Task<TaskResponseDto> GetTaskAsync(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Creator)
                .Include(t => t.Manager)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found");

            return MapToDto(task);
        }

        public async Task<TaskResponseDto> UpdateTaskAsync(int id, TaskUpdateDto dto)
        {
            var task = await _context.Tasks
                .Include(t => t.Creator)
                .Include(t => t.Manager)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found");

            // Логируем изменения
            var changes = new List<string>();

            if (!string.IsNullOrEmpty(dto.Name) && dto.Name != task.Name)
            {
                changes.Add($"Name changed from '{task.Name}' to '{dto.Name}'");
                task.Name = dto.Name;
            }

            if (dto.Description != null && dto.Description != task.Description)
            {
                changes.Add("Description updated");
                task.Description = dto.Description;
            }

            if (dto.ColumnId.HasValue && dto.ColumnId.Value != task.ColumnId)
            {
                changes.Add($"Moved to column ID {dto.ColumnId.Value}");
                task.ColumnId = dto.ColumnId.Value;
            }

            if (dto.ManagerId.HasValue && dto.ManagerId.Value != task.ManagerId)
            {
                changes.Add($"Manager changed to user ID {dto.ManagerId.Value}");
                task.ManagerId = dto.ManagerId.Value;
            }

            task.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Создаем лог, если были изменения
            if (changes.Any())
            {
                await _taskLogService.CreateTaskLogAsync(task.Id, string.Join("; ", changes));
            }

            return MapToDto(task);
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        private async System.Threading.Tasks.Task LoadRelatedData(Task task)
        {
            await _context.Entry(task)
                .Reference(t => t.Creator)
                .LoadAsync();

            await _context.Entry(task)
                .Reference(t => t.Manager)
                .LoadAsync();
        }

        private static TaskResponseDto MapToDto(Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            return new TaskResponseDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                CreatedDate = task.CreatedDate,
                UpdatedDate = task.UpdatedDate,
                ColumnId = task.ColumnId,
                Creator = new UserDto
                {
                    Id = task.Creator.Id,
                    Name = task.Creator.Name,
                    Email = task.Creator.Email,
                    Role = task.Creator.Role?.RoleName ?? "User"
                },
                Manager = task.Manager != null ? new UserDto
                {
                    Id = task.Manager.Id,
                    Name = task.Manager.Name,
                    Email = task.Manager.Email,
                    Role = task.Manager.Role?.RoleName ?? "User"
                } : null
            };
        }
    }
}
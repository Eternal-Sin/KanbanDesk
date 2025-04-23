using KanbanBackend.Data;
using KanbanBackend.Models;
using KanbanBackend.Models.DTOs.TaskLogs;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Services
{
    public class TaskLogService
    {
        private readonly KanbanContext _context;

        public TaskLogService(KanbanContext context)
        {
            _context = context;
        }

        public async Task<List<TaskLogDto>> GetTaskLogsAsync(int taskId)
        {
            return await _context.TaskLogs
                .Where(tl => tl.TaskId == taskId)
                .OrderByDescending(tl => tl.UpdatedDate)
                .Select(tl => new TaskLogDto
                {
                    Id = tl.Id,
                    TaskId = tl.TaskId,
                    UpdatedDate = tl.UpdatedDate,
                    UpdatedAction = tl.UpdatedAction
                })
                .ToListAsync();
        }

        public async Task<TaskLogDto> CreateTaskLogAsync(int taskId, string action)
        {
            var taskLog = new TaskLog
            {
                TaskId = taskId,
                UpdatedDate = DateTime.UtcNow,
                UpdatedAction = action
            };

            _context.TaskLogs.Add(taskLog);
            await _context.SaveChangesAsync();

            return new TaskLogDto
            {
                Id = taskLog.Id,
                TaskId = taskLog.TaskId,
                UpdatedDate = taskLog.UpdatedDate,
                UpdatedAction = taskLog.UpdatedAction
            };
        }

        
    }
}
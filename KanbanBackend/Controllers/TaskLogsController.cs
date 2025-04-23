using KanbanBackend.Models.DTOs.TaskLogs;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers
{
    [ApiController]
    [Route("api/tasks/{taskId}/[controller]")]
    public class TaskLogsController : ControllerBase
    {
        private readonly TaskLogService _taskLogService;

        public TaskLogsController(TaskLogService taskLogService)
        {
            _taskLogService = taskLogService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskLogDto>>> GetTaskLogs(int taskId)
        {
            try
            {
                return await _taskLogService.GetTaskLogsAsync(taskId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskLogDto>> CreateTaskLog(
            int taskId, [FromQuery] string action)
        {
            try
            {
                var taskLog = await _taskLogService.CreateTaskLogAsync(taskId, action);
                return CreatedAtAction(
                    nameof(GetTaskLogs),
                    new { taskId },
                    taskLog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
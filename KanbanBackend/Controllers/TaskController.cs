using KanbanBackend.Models.DTOs.Tasks;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers
{
    [ApiController]
    [Route("api/columns/{columnId}/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(
            TaskService taskService,
            ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask(
            int columnId, [FromBody] TaskCreateDto dto)
        {
            try
            {
                dto.ColumnId = columnId; // ”бедимс€, что задача создаетс€ в нужной колонке
                var task = await _taskService.CreateTaskAsync(dto);
                return CreatedAtAction(
                    nameof(GetTask),
                    new { columnId, id = task.Id },
                    task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDto>> GetTask(int columnId, int id)
        {
            try
            {
                return await _taskService.GetTaskAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting task with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int columnId, int id, [FromBody] TaskUpdateDto dto)
        {
            try
            {
                await _taskService.UpdateTaskAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating task with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int columnId, int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting task with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
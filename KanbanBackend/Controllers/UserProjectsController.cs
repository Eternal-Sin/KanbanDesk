using KanbanBackend.Models.DTOs.UserProjects;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers
{
    [ApiController]
    [Route("api/projects/{projectId}/[controller]")]
    public class UserProjectsController : ControllerBase
    {
        private readonly UserProjectService _userProjectService;

        public UserProjectsController(UserProjectService userProjectService)
        {
            _userProjectService = userProjectService;
        }

        [HttpPost]
        public async Task<ActionResult<UserProjectDto>> AddUserToProject(
            int projectId, [FromBody] UserProjectCreateDto dto)
        {
            try
            {
                dto.ProjectId = projectId; // Убедимся, что ID проекта совпадает
                var result = await _userProjectService.AddUserToProjectAsync(dto);
                return CreatedAtAction(
                    nameof(GetProjectMembers),
                    new { projectId },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UserProjectDto>>> GetProjectMembers(int projectId)
        {
            try
            {
                return await _userProjectService.GetProjectMembersAsync(projectId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveUserFromProject(int projectId, int userId)
        {
            try
            {
                await _userProjectService.RemoveUserFromProjectAsync(userId, projectId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
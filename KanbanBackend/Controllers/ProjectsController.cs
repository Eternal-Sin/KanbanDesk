using KanbanBackend.Models;
using KanbanBackend.Models.DTOs.Projects;
using Microsoft.AspNetCore.Authorization;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _projectService;

    public ProjectsController(ProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Создать новый проект
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> CreateProject(
        [FromBody] ProjectCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var project = await _projectService.CreateProjectAsync(dto, userId);

        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    /// <summary>
    /// Получить проект по ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponseDto>> GetProject(int id)
    {
        var project = await _projectService.GetProjectAsync(id);
        return Ok(project);
    }
}
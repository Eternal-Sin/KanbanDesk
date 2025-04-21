using KanbanBackend.Models.DTOs.Columns;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/projects/{projectId}/[controller]")]
public class ColumnsController : ControllerBase
{
    private readonly ColumnService _columnService;

    public ColumnsController(ColumnService columnService)
    {
        _columnService = columnService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ColumnDto>>> GetColumns(int projectId)
    {
        return Ok(await _columnService.GetColumnsAsync(projectId));
    }

    [HttpPost]
    public async Task<ActionResult<ColumnDto>> CreateColumn(int projectId, ColumnCreateDto dto)
    {
        dto.ProjectId = projectId; // Убедимся, что ProjectId совпадает
        var column = await _columnService.CreateColumnAsync(dto);
        return CreatedAtAction(nameof(GetColumns), new { projectId }, column);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateColumn(int id, ColumnUpdateDto dto)
    {
        await _columnService.UpdateColumnAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteColumn(int id)
    {
        await _columnService.DeleteColumnAsync(id);
        return NoContent();
    }
}
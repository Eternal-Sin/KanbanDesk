namespace KanbanBackend.Models.DTOs.Columns;

public class ColumnCreateDto
{
    public required string Name { get; set; }
    public required int ProjectId { get; set; }
    public int Order { get; set; } = 0;
}
using KanbanBackend.Data;
using KanbanBackend.Models.DTOs.Columns;
using KanbanBackend.Models;
using Microsoft.EntityFrameworkCore;

public class ColumnService
{
    private readonly KanbanContext _context;

    public ColumnService(KanbanContext context)
    {
        _context = context;
    }

    public async Task<List<ColumnDto>> GetColumnsAsync(int projectId)
    {
        return await _context.Columns
            .Where(c => c.ProjectId == projectId)
            .OrderBy(c => c.Order)
            .Select(c => new ColumnDto
            {
                Id = c.Id,
                Name = c.Name,
                Order = c.Order,
                ProjectId = c.ProjectId
            }).ToListAsync(); 
    }

    public async Task<ColumnDto> CreateColumnAsync(ColumnCreateDto dto)
    {
        var column = new Column
        {
            Name = dto.Name,
            ProjectId = dto.ProjectId,
            Order = dto.Order
        };

        _context.Columns.Add(column);
        await _context.SaveChangesAsync();

        return new ColumnDto
        {
            Id = column.Id,
            Name = column.Name,
            Order = column.Order,
            ProjectId = column.ProjectId
        };
    }

    public async Task<ColumnDto> UpdateColumnAsync(int id, ColumnUpdateDto dto)
    {
        var column = await _context.Columns.FindAsync(id);
        if (column == null)
            throw new Exception("Column not found");

        if (!string.IsNullOrEmpty(dto.Name))
            column.Name = dto.Name;

        if (dto.Order.HasValue)
            column.Order = dto.Order.Value;

        await _context.SaveChangesAsync();

        return new ColumnDto
        {
            Id = column.Id,
            Name = column.Name,
            Order = column.Order,
            ProjectId = column.ProjectId
        };
    }

    public async Task<ColumnDto> DeleteColumnAsync(int id)
    {
        var column = await _context.Columns.FindAsync(id);
        if (column == null)
            throw new Exception("Column not found");

        var result = new ColumnDto
        {
            Id = column.Id,
            Name = column.Name,
            Order = column.Order,
            ProjectId = column.ProjectId
        };

        _context.Columns.Remove(column);
        await _context.SaveChangesAsync();

        return result;
    }

}
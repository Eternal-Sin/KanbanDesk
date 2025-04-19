using KanbanBackend.Data;
using KanbanBackend.Models;
using KanbanBackend.Models.DTOs.Projects;
using KanbanBackend.Models.DTOs.Users;
using Microsoft.EntityFrameworkCore;
namespace KanbanBackend.Services

{
    public class ProjectService
    {
        private readonly KanbanContext _context;

        public ProjectService(KanbanContext context)
        {
            _context = context;
        }

        public async Task<ProjectResponseDto> CreateProjectAsync(ProjectCreateDto dto, int userId)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatorId = userId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // Load creator with role
            await _context.Entry(project)
                .Reference(p => p.Creator)
                .Query()
                .Include(c => c.Role)
                .LoadAsync();

            return MapToProjectResponseDto(project);
        }

        public async Task<ProjectResponseDto> GetProjectAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Creator)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                throw new Exception("Project not found");

            return MapToProjectResponseDto(project);
        }

        private static ProjectResponseDto MapToProjectResponseDto(Project project) => new()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedDate = project.CreatedDate,
            Creator = new UserDto
            {
                Id = project.Creator.Id,
                Name = project.Creator.Name,
                Email = project.Creator.Email,
                Role = project.Creator.Role?.RoleName ?? "User"
            }
        };
    }
}
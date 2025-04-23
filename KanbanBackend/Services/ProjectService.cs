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

        public async Task<List<ProjectResponseDto>> GetAllProjectsAsync()
        {
            try
            {
                return await _context.Projects
                    .Include(p => p.Creator)
                    .ThenInclude(c => c.Role)
                    .Select(p => MapToProjectResponseDto(p))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve projects", ex);
            }
        }

        public async Task<ProjectResponseDto> GetProjectAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Creator)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(p => p.Id == id);

            return project == null
                ? throw new KeyNotFoundException($"Project with ID {id} not found")
                : MapToProjectResponseDto(project);
        }

        public async Task<ProjectResponseDto> CreateProjectAsync(ProjectCreateDto dto, int creatorId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Проверяем существование пользователя
                var creator = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == creatorId);

                if (creator == null)
                    throw new ArgumentException($"User with ID {creatorId} not found");

                var project = new Project
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    CreatorId = creatorId,
                    CreatedDate = DateTime.UtcNow
                };

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Намеренно не используем Include, а подгружаем отдельно
                project.Creator = creator;
                return MapToProjectResponseDto(project);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ProjectResponseDto> UpdateProjectAsync(int id, ProjectUpdateDto dto)
        {
            var project = await _context.Projects
                .Include(p => p.Creator)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                throw new Exception("Project not found");

            if (!string.IsNullOrEmpty(dto.Name))
                project.Name = dto.Name;

            if (dto.Description != null)
                project.Description = dto.Description;

            project.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return MapToProjectResponseDto(project); 
        }

        public async Task<ProjectResponseDto> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Creator)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                throw new Exception("Project not found");

            var result = MapToProjectResponseDto(project); 
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return result; 
        }
        public async Task<List<ProjectResponseDto>> GetProjectsByUserAsync(int userId)
        {
            return await _context.UserProjects
                .Where(up => up.UserId == userId)
                .Include(up => up.Project)
                .ThenInclude(p => p.Creator)
                .ThenInclude(c => c.Role)
                .Select(up => MapToProjectResponseDto(up.Project))
                .ToListAsync();
        }

        public async Task<int> GetProjectCountAsync()
        {
            return await _context.Projects.CountAsync();
        }
        private static ProjectResponseDto MapToProjectResponseDto(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project), "Project is null");

            if (project.Creator == null)
                throw new InvalidOperationException("Project Creator is null");

            return new ProjectResponseDto
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
}
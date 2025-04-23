using KanbanBackend.Data;
using KanbanBackend.Models;
using KanbanBackend.Models.DTOs.UserProjects;
using KanbanBackend.Models.DTOs.Users;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Services
{
    public class UserProjectService
    {
        private readonly KanbanContext _context;

        public UserProjectService(KanbanContext context)
        {
            _context = context;
        }

        public async Task<UserProjectDto> AddUserToProjectAsync(UserProjectCreateDto dto)
        {
            // ѕровер€ем существование пользовател€ и проекта
            var user = await _context.Users.FindAsync(dto.UserId);
            var project = await _context.Projects.FindAsync(dto.ProjectId);

            if (user == null || project == null)
                throw new ArgumentException("User or Project not found");

            var userProject = new UserProject
            {
                UserId = dto.UserId,
                ProjectId = dto.ProjectId
            };

            _context.UserProjects.Add(userProject);
            await _context.SaveChangesAsync();

            // «агружаем св€занные данные
            await _context.Entry(userProject)
                .Reference(up => up.User)
                .Query()
                .Include(u => u.Role)
                .LoadAsync();

            return new UserProjectDto
            {
                UserId = userProject.UserId,
                ProjectId = userProject.ProjectId,
                User = new UserDto
                {
                    Id = userProject.User.Id,
                    Name = userProject.User.Name,
                    Email = userProject.User.Email,
                    Role = userProject.User.Role?.RoleName ?? "User"
                }
            };
        }

        public async Task<List<UserProjectDto>> GetProjectMembersAsync(int projectId)
        {
            return await _context.UserProjects
                .Where(up => up.ProjectId == projectId)
                .Include(up => up.User)
                .ThenInclude(u => u.Role)
                .Select(up => new UserProjectDto
                {
                    UserId = up.UserId,
                    ProjectId = up.ProjectId,
                    User = new UserDto
                    {
                        Id = up.User.Id,
                        Name = up.User.Name,
                        Email = up.User.Email,
                        Role = up.User.Role != null ? up.User.Role.RoleName : "User"
                    }
                })
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task RemoveUserFromProjectAsync(int userId, int projectId)
        {
            var userProject = await _context.UserProjects
                .FirstOrDefaultAsync(up => up.UserId == userId && up.ProjectId == projectId);

            if (userProject == null)
            {
                throw new KeyNotFoundException("User is not a member of this project");
            }

            _context.UserProjects.Remove(userProject);
            await _context.SaveChangesAsync();
        }
    }
}
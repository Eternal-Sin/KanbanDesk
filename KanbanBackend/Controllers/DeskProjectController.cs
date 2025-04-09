using Microsoft.AspNetCore.Mvc;
using KanbanBackend.Data;
using KanbanBackend.Models;

namespace KanbanBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeskProjectController : ControllerBase
    {
        private readonly KanbanContext _context;

        public DeskProjectController(KanbanContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.DeskProjects);
        }

        [HttpPost]
        public IActionResult Post(DeskProject project)
        {
            _context.DeskProjects.Add(project);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = project.id }, project);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, DeskProject updatedProject)
        {
            var project = _context.DeskProjects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            project.name = updatedProject.name;
            project.description = updatedProject.description;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _context.DeskProjects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.DeskProjects.Remove(project);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Project;
using PortfolioAPI.Models.Data;

namespace PortfolioAPI.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : BaseController
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectResponseDto>>> GetAllProjects()
        {
            var projects = await _projectService.GetProjects();

            if (projects == null || projects.Count == 0)
            {
                return NotFound();
            }

            return Ok(projects);
        }
    }
}

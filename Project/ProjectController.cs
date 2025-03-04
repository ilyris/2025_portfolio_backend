using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Project;
using PortfolioAPI.Models.Data;
using PortfolioAPI.Services;

namespace PortfolioAPI.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : BaseController
    {
        private readonly ProjectService _projectService;
        private readonly IS3Service _s3Service;

        public ProjectController(ProjectService projectService, S3Service s3Service)
        {
            _projectService = projectService;
            _s3Service = s3Service;

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
        [HttpGet("get-technologies")]
        public async Task<ActionResult<List<ProjectResponseDto>>> GetTechnologies()
        {
            var technologies = await _projectService.GetTechnologies();

            if (technologies == null || technologies.Count == 0)
            {
                return NotFound();
            }

            return Ok(technologies);
        }

        [HttpGet("get-presigned-url")]
        public IActionResult GetPresignedUrl([FromQuery] string fileName, [FromQuery] string contentType)
        {
            try
            {
                var presignedUrl = _s3Service.GeneratePresignedUrl(fileName, contentType);
                return Ok(new { url = presignedUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Error generating pre-signed URL", details = ex.Message });
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddProject([FromBody] ProjectCreationRequestDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _projectService.AddProject(projectDto);

            if (result.GetType().GetProperty("error") != null) // Dynamically check if 'error' exists
            {
                return BadRequest(result);
            }

            return Ok(result);
        }



    }
}

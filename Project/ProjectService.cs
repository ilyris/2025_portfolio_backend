using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models.Data;

namespace PortfolioAPI.Project;

public class ProjectService
{
    private readonly AppDbContext _dbContext;

    public ProjectService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ProjectResponseDto>> GetProjects()
    {
        var projects = await _dbContext.Projects  // ✅ Now referencing PortfolioProject
            .Include(p => p.ProjectTechnologies)
                .ThenInclude(pt => pt.Technology)
            .Select(p => new ProjectResponseDto
            {
                Title = p.Title,
                Description = p.Description,
                Image = p.Image,
                Github = p.Github,
                WebsiteUrl = p.WebsiteUrl,
                Technologies = p.ProjectTechnologies
                    .Select(pt => new TechnologyDto
                    {
                        Id = pt.Technology.Id,
                        Name = pt.Technology.Name
                    }).ToList()
            })
            .ToListAsync();

        return projects;
    }
}

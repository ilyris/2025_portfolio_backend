using System.Transactions; // ✅ Import Transactions
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

    public async Task<List<TechnologyDto>> GetTechnologies()
    {
        var technologies = await _dbContext.Technologies
                    .Select(technology => new TechnologyDto
                    {
                        Id = technology.Id,
                        Name = technology.Name
                    }).ToListAsync();

        return technologies;
    }

    public async Task<object> AddProject(ProjectCreationRequestDto projectDto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            Console.WriteLine(projectDto);

            var newProject = new PortfolioProject
            {
                Title = projectDto.Title,
                Description = projectDto.Description,
                Github = projectDto.Github,
                WebsiteUrl = projectDto.WebsiteUrl,
                Image = string.IsNullOrEmpty(projectDto.ImageUrl) ? null : projectDto.ImageUrl,
            };

            _dbContext.Projects.Add(newProject);
            await _dbContext.SaveChangesAsync();

            var newProjectTechnologies = projectDto.Technologies
                .Select(tech => new ProjectTechnologies
                {
                    ProjectId = newProject.Id,
                    TechnologyId = tech.Id
                })
                .ToList();


            if (newProjectTechnologies.Count > 0)
            {
                await _dbContext.ProjectTechnologies.AddRangeAsync(newProjectTechnologies);
                await _dbContext.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return new { message = "Project added successfully", projectId = newProject.Id };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine("Error adding project: " + ex.Message);
            return new { error = "Failed to add project", details = ex.Message };
        }
    }

}

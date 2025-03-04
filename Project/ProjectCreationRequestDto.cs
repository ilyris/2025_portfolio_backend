using PortfolioAPI.Project;

public class ProjectCreationRequestDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Image { get; set; }
    public required string ImageUrl { get; set; }
    public string? Github { get; set; }
    public required string WebsiteUrl { get; set; } // Remove nullable (`string?`)
    public required List<TechnologyDto> Technologies { get; set; }
}
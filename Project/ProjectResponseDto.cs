namespace PortfolioAPI.Project;

public class ProjectResponseDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Image { get; set; }
    public string? Github { get; set; }
    public string? WebsiteUrl { get; set; }
    public List<TechnologyDto> Technologies { get; set; } = new();
}

public class TechnologyDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

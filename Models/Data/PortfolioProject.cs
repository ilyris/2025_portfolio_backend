using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioAPI.Models.Data;

public class PortfolioProject  // ✅ Rename class to avoid conflicts
{
    [Key]
    [Column("id")]

    public int Id { get; set; }
    [Column("title")]
    public required string Title { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("image")]
    public string? Image { get; set; }
    [Column("github")]
    public string? Github { get; set; }
    [Column("website_url")]
    public string? WebsiteUrl { get; set; }

    public List<ProjectTechnologies> ProjectTechnologies { get; set; } = new();
}

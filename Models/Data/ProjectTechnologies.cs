using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioAPI.Models.Data;

public class ProjectTechnologies
{
    [Key]
    [Column("project_id")]
    public int ProjectId { get; set; }

    [Key]
    [Column("technology_id")]
    public int TechnologyId { get; set; }

    public PortfolioProject? Project { get; set; }
    public Technology? Technology { get; set; }
}
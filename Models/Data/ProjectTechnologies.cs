using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioAPI.Models.Data;

public class ProjectTechnologies
{

    [Key]
    [Column("project_id")]  // ✅ Ensure it matches PostgreSQL column name exactly
    public int ProjectId { get; set; }

    [Column("technology_id")]  // ✅ Ensure it matches PostgreSQL column name exactly
    public int TechnologyId { get; set; }

    [ForeignKey("ProjectId")]
    public PortfolioProject Project { get; set; }

    [ForeignKey("TechnologyId")]
    public Technology Technology { get; set; }
}

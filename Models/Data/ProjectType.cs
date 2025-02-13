using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PortfolioAPI.Models.Data;


public partial class ProjectType
{
    [Key]
    public string Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public string Github { get; set; } = null!;
    public string WebsiteUrl { get; set; }

}

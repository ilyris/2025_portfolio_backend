using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models.Data;

namespace PortfolioAPI;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PortfolioProject> Projects { get; set; }  // ✅ Fix: Use renamed class
    public DbSet<ProjectTechnologies> ProjectTechnologies { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioProject>().ToTable("projects");
        modelBuilder.Entity<ProjectTechnologies>().ToTable("project_technologies")
                .HasKey(pt => new { pt.ProjectId, pt.TechnologyId });
        modelBuilder.Entity<Technology>().ToTable("technologies");
        modelBuilder.Entity<User>().ToTable("users");

        base.OnModelCreating(modelBuilder);
    }
}

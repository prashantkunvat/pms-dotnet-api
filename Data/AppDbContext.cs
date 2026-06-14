using Microsoft.EntityFrameworkCore;
using PMS.Api.Models;

namespace PMS.Api.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<ProductCategory> ProductCategories {get; set;}

    public DbSet<Product> Products {get; set;}

    public DbSet<Project> Projects {get; set;}

    public DbSet<ProjectTask> ProjectTasks {get; set;}

    public DbSet<User> Users {get; set;}

    // Global Qeury Filter For soft delete
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductCategory>()
            .HasQueryFilter(c => !c.IsDeleted);

        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.IsDeleted);

        modelBuilder.Entity<Project>()
            .HasQueryFilter(p => !p.IsDeleted);

        modelBuilder.Entity<ProjectTask>()
            .HasQueryFilter(p => !p.IsDeleted);
    }
}

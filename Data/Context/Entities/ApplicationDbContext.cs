using Microsoft.EntityFrameworkCore;

namespace Data.Context.Entities;

public partial class ApplicationDbContext : DbContext
{
    public DbSet<GROUP> Groups { get; set; } = null!;
    public DbSet<USER> Users { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

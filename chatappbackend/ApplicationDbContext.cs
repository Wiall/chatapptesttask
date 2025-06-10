using Microsoft.EntityFrameworkCore;

namespace chatappbackend;

public class ApplicationDbContext : DbContext
{
    public DbSet<Message>? Messages { get; set; }
    public DbSet<AnalysisResult>? AnalysisResults { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>()
            .HasMany<AnalysisResult>()
            .WithOne()
            .HasForeignKey(ar => ar.MessageID);
    }
}
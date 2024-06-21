using Microsoft.EntityFrameworkCore;
using Services.Entities.Base;
using Services.Entities.Logs;

namespace Services;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }
    
    public DbSet<LogChannel> LogChannels { get; set; }
    public DbSet<LogCommand> LogCommands { get; set; }
    public DbSet<LogCommandOption> LogCommandOptions { get; set; }
    public DbSet<LogGuild> LogGuilds { get; set; }
    public DbSet<LogUser> LogUsers { get; set; }
    public DbSet<LogGuildUser> LogGuildUsers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=15432;Username=postgres;Password=password;Database=UnitForumParser;Pooling=true");
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ModifyAuditableEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ModifyAuditableEntities()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(x => x.State is EntityState.Added or EntityState.Modified);

        var time = DateTime.UtcNow;
        
        foreach (var entry in entries)
        {
            if (entry.Entity is not Auditable) continue;

            var property = entry.State switch
            {
                EntityState.Added => entry.Property(nameof(Auditable.EntityCreatedAt)),
                _ => entry.Property(nameof(Auditable.EntityModifiedAt)),
            };

            property.CurrentValue ??= time;
        }
    }
}
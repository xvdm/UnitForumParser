using Microsoft.EntityFrameworkCore;
using Services.Entities.Logs;

namespace Services;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }
    
    public DbSet<LogChannel> LogChannels { get; set; }
    public DbSet<LogCommand> LogCommands { get; set; }
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
}
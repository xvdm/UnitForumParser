using Domain.Entities.Base;

namespace Domain.Entities.Logs;

/// <summary>
/// Discord server
/// </summary>
public sealed class LogGuild : Auditable
{
    public ulong Id { get; set; }
    public string? Name { get; set; }
    public string? IconUrl { get; set; }
    
    public ICollection<LogChannel> LogChannels { get; set; } = null!;
    public ICollection<LogGuildUser> LogGuildUsers { get; set; } = null!;
}
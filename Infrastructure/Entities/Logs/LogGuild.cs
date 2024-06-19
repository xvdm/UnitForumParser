namespace Services.Entities.Logs;

/// <summary>
/// Discord server
/// </summary>
public sealed class LogGuild
{
    public ulong Id { get; set; }
    public string? Name { get; set; }
    public string? IconUrl { get; set; }
    
    /// <summary>
    /// Last time LogGuild entity was modified
    /// </summary>
    public DateTime ModifiedAt { get; set; }
    
    public ICollection<LogChannel> LogChannels { get; set; } = null!;
    public ICollection<LogGuildUser> LogGuildUsers { get; set; } = null!;
}
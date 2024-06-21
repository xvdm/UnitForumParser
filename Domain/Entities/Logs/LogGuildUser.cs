namespace Domain.Entities.Logs;

/// <summary>
/// User in guild
/// </summary>
public sealed class LogGuildUser
{
    public Guid Id { get; set; }
    
    public ulong LogUserId { get; set; }
    public LogUser LogUser { get; set; } = null!;
    
    public ulong LogGuildId { get; set; }
    public LogGuild LogGuild { get; set; } = null!;

    public string? GuildNickname { get; set; }
    
    /// <summary>
    /// When user joined guild
    /// </summary>
    public DateTimeOffset? JoinedAt { get; set; }
}
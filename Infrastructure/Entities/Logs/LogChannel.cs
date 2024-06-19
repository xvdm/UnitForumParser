namespace Services.Entities.Logs;

/// <summary>
/// Direct messages or text channel on server
/// </summary>
public sealed class LogChannel
{
    /// <summary>
    /// Id of the text channel on server or id of direct messages
    /// </summary>
    public ulong Id { get; set; } 
    
    /// <summary>
    /// Name of text channel on server or @username if in direct messages
    /// </summary>
    public string? Name { get; set; } 
    
    /// <summary>
    /// Shows if this channel is still in guild (was not deleted)
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Type of the channel: text, audio or else. From Discord.ChannelType enum
    /// </summary>
    public int? Type { get; set; }
    
    /// <summary>
    /// Id of the server this channel belongs to
    /// </summary>
    public ulong? LogGuildId { get; set; }
    public LogGuild? LogGuild { get; set; }

    public ICollection<LogCommand> LogCommands { get; set; } = null!;
}
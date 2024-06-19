namespace Services.Entities.Logs;

public sealed class LogUser
{
    public ulong Id { get; set; }
    public string? Username { get; set; }
    public string? GlobalName { get; set; }
    public string? AvatarUrl { get; set; }
    
    public ICollection<LogGuildUser> LogGuildUsers { get; set; } = null!;
    public ICollection<LogCommand> LogCommands { get; set; } = null!;
}
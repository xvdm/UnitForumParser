namespace Services.Entities.Logs;

public sealed class LogCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public ulong LogUserId { get; set; }
    public LogUser LogUser { get; set; } = null!;
    
    public ulong LogChannelId { get; set; }
    public LogChannel LogChannel { get; set; } = null!;
    
    public ICollection<LogCommandOption> LogCommandOptions { get; set; } = null!;
}
namespace Services.Entities.Logs;

public sealed class LogCommandOption
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Value { get; set; }
    
    public Guid LogCommandId { get; set; }
    public LogCommand LogCommand { get; set; } = null!;
}
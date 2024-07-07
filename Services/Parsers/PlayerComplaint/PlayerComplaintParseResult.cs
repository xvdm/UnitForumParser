namespace Services.Parsers.PlayerComplaint;

public sealed record PlayerComplaintParseResult
{
    public string? AuthorNickname { get; set; }
    public string? AuthorId { get; set; }
    public string? IntruderNickname { get; set; }
    public List<string> IntruderIds { get; set; } = null!;
    public string? ViolationDescription { get; set; }
    public string? ViolationDateTime { get; set; }
    public string? ViolationProofs { get; set; }
}
namespace Services.Parsers.PlayerComplaint;

public sealed record PlayerComplaintParseResult
{
    public string? AuthorNickname { get; set; }
    public string? IntruderNickname { get; set; }
    public string? IntruderStatic { get; set; }
    public string? ViolationDescription { get; set; }
    public string? ViolationDateTime { get; set; }
    public string? ViolationProofs { get; set; }
}
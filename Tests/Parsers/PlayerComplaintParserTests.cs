using Services.Parsers.PlayerComplaint;

namespace Tests.Parsers;

public class PlayerComplaintParserTests
{
    private readonly PlayerComplaintParser _parser = new();

    [Theory]
    [MemberData(nameof(CorrectFullData))]
    public void ParserShouldRetrieveAllDataFromComplaint(string complaint, PlayerComplaintParseResult expected)
    {
        var result = _parser.Parse(complaint);
        Assert.Equivalent(expected, result);
    }

    public static IEnumerable<object[]> CorrectFullData()
    {
        yield return 
        [
            "Author Nickname\n" +
            "1122\n" +
            "Intruder Nickname 12345\n" +
            "Violation Description\n" +
            "01.01.2000\n" +
            "Violation Proofs", 
            new PlayerComplaintParseResult
            {
                AuthorNickname = "Author Nickname", 
                AuthorId = "1122",
                IntruderNickname = "Intruder Nickname", 
                IntruderId = "12345", 
                ViolationDescription = "Violation Description", 
                ViolationDateTime = "01.01.2000",
                ViolationProofs = "Violation Proofs"
            }
        ];
        yield return 
        [
            "1. Author Nickname\n" +
            "2. 1122\n" +
            "3. Intruder Nickname 12345\n" +
            "4. Violation Description\n" +
            "5. 01.01.2000\n" +
            "6. Violation Proofs", 
            new PlayerComplaintParseResult
            {
                AuthorNickname = "Author Nickname", 
                AuthorId = "1122",
                IntruderNickname = "Intruder Nickname", 
                IntruderId = "12345", 
                ViolationDescription = "Violation Description", 
                ViolationDateTime = "01.01.2000",
                ViolationProofs = "Violation Proofs"
            }
        ];
        yield return 
        [
            "- Author Nickname\n" +
            "- 1122\n" +
            "- Intruder Nickname 12345\n" +
            "- Violation Description\n" +
            "- 01.01.2000\n" +
            "- Violation Proofs", 
            new PlayerComplaintParseResult
            {
                AuthorNickname = "Author Nickname",
                AuthorId = "1122",
                IntruderNickname = "Intruder Nickname", 
                IntruderId = "12345", 
                ViolationDescription = "Violation Description", 
                ViolationDateTime = "01.01.2000",
                ViolationProofs = "Violation Proofs"
            }
        ];
        yield return 
        [
            "Ваш нікнейм: Author Nickname\n" +
            "Ваш ID: 1122\n" +
            "Нікнейм/ID порушника: Intruder Nickname 12345" +
            "Детальний опис ситуації: Violation Description\n" +
            "Дата і час події: 01.01.2000\n" +
            "Докази порушення: Violation Proofs\n" +
            "Тайм-код порушення: 1:20", 
            new PlayerComplaintParseResult
            {
                AuthorNickname = "Author Nickname",
                AuthorId = "1122",
                IntruderNickname = "Intruder Nickname", 
                IntruderId = "12345", 
                ViolationDescription = "Violation Description", 
                ViolationDateTime = "01.01.2000",
                ViolationProofs = "Violation Proofs"
            }
        ];
    }
}
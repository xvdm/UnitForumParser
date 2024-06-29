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
            "Intruder Nickname\n" +
            "12345\n" +
            "Violation Description\n" +
            "01.01.2000\n" +
            "Violation Proofs", 
            new PlayerComplaintParseResult
            {
                AuthorNickname = "Author Nickname", 
                IntruderNickname = "Intruder Nickname", 
                IntruderStatic = "12345", 
                ViolationDescription = "Violation Description", 
                ViolationDateTime = "01.01.2000",
                ViolationProofs = "Violation Proofs"
            }
        ];
        yield return 
        [
            "1. Author Nickname\n" +
            "2. Intruder Nickname\n" +
            "3. 12345\n" +
            "4. Violation Description\n" +
            "5. 01.01.2000\n" +
            "6. Violation Proofs", 
            new PlayerComplaintParseResult
            {
                AuthorNickname = "Author Nickname", 
                IntruderNickname = "Intruder Nickname", 
                IntruderStatic = "12345", 
                ViolationDescription = "Violation Description", 
                ViolationDateTime = "01.01.2000",
                ViolationProofs = "Violation Proofs"
            }
        ];
        yield return 
        [
            "- Author Nickname\n" +
            "- Intruder Nickname\n" +
            "- 12345\n" +
            "- Violation Description\n" +
            "- 01.01.2000\n" +
            "- Violation Proofs", 
            new PlayerComplaintParseResult
            {
                AuthorNickname = "Author Nickname", 
                IntruderNickname = "Intruder Nickname", 
                IntruderStatic = "12345", 
                ViolationDescription = "Violation Description", 
                ViolationDateTime = "01.01.2000",
                ViolationProofs = "Violation Proofs"
            }
        ];
        yield return 
        [
            "Ваш нікнейм: Author Nickname\n" +
            "Нікнейм/ID порушника: Intruder Nickname\n" +
            "Детальний опис ситуації: 12345\n" +
            "Дата і час події: Violation Description\n" +
            "Докази порушення: 01.01.2000\n" +
            "Тайм-код порушення: Violation Proofs", 
            new PlayerComplaintParseResult
            {
                AuthorNickname = "Author Nickname", 
                IntruderNickname = "Intruder Nickname", 
                IntruderStatic = "12345", 
                ViolationDescription = "Violation Description", 
                ViolationDateTime = "01.01.2000",
                ViolationProofs = "Violation Proofs"
            }
        ];
    }
}
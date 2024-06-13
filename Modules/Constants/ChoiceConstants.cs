using Discord;

namespace Modules.Constants;

public static class ChoiceConstants
{
    public static List<ApplicationCommandOptionChoiceProperties> Servers =>
    [
        new ApplicationCommandOptionChoiceProperties
        {
            Name = "Сервер №1",
            Value = "1"
        }
    ];
}
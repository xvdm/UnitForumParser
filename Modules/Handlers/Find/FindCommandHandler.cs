using Discord;
using Discord.WebSocket;
using Modules.Constants;

namespace Modules.Handlers.Find;

public sealed class FindCommandHandler
{
    public async Task HandleFindCommand(SocketSlashCommand command)
    {
        var fieldName = command.Data.Options.First().Name;
        switch(fieldName)
        {
            case "static":
                await HandleFindByStaticCommand(command);
                break;
            case "forum":
                await HandleFindByForumCommand(command);
                break;
            case "nickname":
                await HandleFindByNicknameCommand(command);
                break;
        }
    }
    
    private async Task HandleFindByStaticCommand(SocketSlashCommand command)
    {
        var staticId = command.Data.Options.First().Options.First(x => x.Name == "static").Value;
        var server = command.Data.Options.First().Options.First(x => x.Name == "server").Value;

        var description = $"**Static ID**: ```{staticId}```" +
                          $"**Сервер**: ```{server}```";
        
        var embedBuilder = new EmbedBuilder()
            .WithTitle($"Інформація по статику {staticId}")
            .WithDescription(description)
            .WithColor(CommonConstants.DefaultMessageColor)
            .WithFooter(CommonConstants.DeveloperSignature);

        await command.RespondAsync(embed: embedBuilder.Build());
    }
    
    private async Task HandleFindByForumCommand(SocketSlashCommand command)
    {
        var forumId = command.Data.Options.First().Options.First(x => x.Name == "forum-id").Value;

        var description = $"**Forum ID**: ```{forumId}```";
        
        var embedBuilder = new EmbedBuilder()
            .WithTitle($"Інформація по профілю {forumId}")
            .WithDescription(description)
            .WithColor(CommonConstants.DefaultMessageColor)
            .WithFooter(CommonConstants.DeveloperSignature);

        await command.RespondAsync(embed: embedBuilder.Build());
    }
    
    private async Task HandleFindByNicknameCommand(SocketSlashCommand command)
    {
        var nickname = command.Data.Options.First().Options.First(x => x.Name == "nickname").Value;
        var server = command.Data.Options.First().Options.First(x => x.Name == "server").Value;

        var description = $"**Нікнейм**: ```{nickname}```" +
                          $"**Сервер**: ```{server}```";
        
        var embedBuilder = new EmbedBuilder()
            .WithTitle($"Інформація по нікнейму {nickname}")
            .WithDescription(description)
            .WithColor(CommonConstants.DefaultMessageColor)
            .WithFooter(CommonConstants.DeveloperSignature);

        await command.RespondAsync(embed: embedBuilder.Build());
    }
}
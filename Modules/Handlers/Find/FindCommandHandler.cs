using Discord;
using Discord.WebSocket;
using Modules.Constants;

namespace Modules.Handlers.Find;

public sealed class FindCommandHandler
{
    public async Task HandleFindStaticCommand(SocketSlashCommand command)
    {
        var staticId = command.Data.Options.First(x => x.Name == "static").Value;
        var server = command.Data.Options.First(x => x.Name == "server").Value;

        var description = $"**Static ID**: ```{staticId}```" +
                          $"**Сервер**: ```{server}```";
        
        var embedBuilder = new EmbedBuilder()
            .WithTitle($"Інформація по статику {staticId}")
            .WithDescription(description)
            .WithColor(CommonConstants.DefaultMessageColor)
            .WithFooter(CommonConstants.DeveloperSignature);

        await command.RespondAsync(embed: embedBuilder.Build());
    }
    
    public async Task HandleFindForumCommand(SocketSlashCommand command)
    {
        var forumId = command.Data.Options.First(x => x.Name == "id").Value;

        var description = $"**Forum ID**: ```{forumId}```";
        
        var embedBuilder = new EmbedBuilder()
            .WithTitle($"Інформація по профілю {forumId}")
            .WithDescription(description)
            .WithColor(CommonConstants.DefaultMessageColor)
            .WithFooter(CommonConstants.DeveloperSignature);

        await command.RespondAsync(embed: embedBuilder.Build());
    }
    
    public async Task HandleFindNicknameCommand(SocketSlashCommand command)
    {
        var nickname = command.Data.Options.First(x => x.Name == "nickname").Value;
        var server = command.Data.Options.First(x => x.Name == "server").Value;

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
using Discord;
using Discord.WebSocket;
using Modules.Constants;

namespace Modules;

public class SlashCommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly DateTime _startTime;
    
    public SlashCommandHandler(DiscordSocketClient client)
    {
        _client = client;
        _startTime = DateTime.UtcNow;
    }
    
    public async Task HandleCommand(SocketSlashCommand command)
    {
        switch(command.Data.Name)
        {
            case "ping":
                await HandlePingCommand(command);
                break;
            case "find":
                await HandleFindCommand(command);
                break;
        }
    }

    private async Task HandlePingCommand(SocketSlashCommand command)
    {
        var uptime = DateTime.UtcNow - _startTime;
        var latency = _client.Latency;
        var status = _client.Status;

        var description = $"**Uptime**: ```{uptime.Days} days {uptime.Hours} hours {uptime.Minutes} minutes {uptime.Seconds} seconds```" +
                          $"**Status**: ```{status}```" +
                          $"**Latency**: ```{latency} ms```";
        
        var embedBuilder = new EmbedBuilder()
            .WithTitle("Перевірка боту")
            .WithDescription(description)
            .WithColor(CommonConstants.DefaultMessageColor)
            .WithFooter(CommonConstants.DeveloperSignature);

        await command.RespondAsync(embed: embedBuilder.Build(), ephemeral: true);
    }

    private async Task HandleFindCommand(SocketSlashCommand command)
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
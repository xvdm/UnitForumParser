using Discord;
using Discord.WebSocket;
using Modules.Constants;

namespace Modules.Handlers.Ping;

public sealed class PingCommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly DateTime _startTime;
    
    public PingCommandHandler(DiscordSocketClient client, DateTime startTime)
    {
        _client = client;
        _startTime = startTime;
    }
    
    public async Task HandlePingCommand(SocketSlashCommand command)
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
}
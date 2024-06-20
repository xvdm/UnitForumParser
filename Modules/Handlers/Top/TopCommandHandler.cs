using Discord;
using Discord.WebSocket;
using Modules.Constants;

namespace Modules.Handlers.Top;

public sealed class TopCommandHandler
{
    public async Task HandleTopIntruderCommand(SocketSlashCommand command)
    {
        var server = command.Data.Options.First(x => x.Name == "server").Value;

        var description = "**1 місце - еммерсон - 228 скарг**";
        
        var embedBuilder = new EmbedBuilder()
            .WithTitle($"Топ форумних губок на сервері {server}")
            .WithDescription(description)
            .WithColor(CommonConstants.DefaultMessageColor)
            .WithFooter(CommonConstants.DeveloperSignature);

        await command.RespondAsync(embed: embedBuilder.Build());
    }
    
    public async Task HandleTopVictimCommand(SocketSlashCommand command)
    {
        var server = command.Data.Options.First(x => x.Name == "server").Value;

        var description = "**1 місце - еммерсон - 1337 скарг**";
        
        var embedBuilder = new EmbedBuilder()
            .WithTitle($"Топ форумних бійців на сервері {server}")
            .WithDescription(description)
            .WithColor(CommonConstants.DefaultMessageColor)
            .WithFooter(CommonConstants.DeveloperSignature);

        await command.RespondAsync(embed: embedBuilder.Build());
    }
}
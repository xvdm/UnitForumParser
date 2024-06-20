using Discord.WebSocket;
using Modules.Handlers.Find;
using Modules.Handlers.Ping;
using Modules.Handlers.Top;
using Services.Repositories;

namespace Modules.Handlers;

public class SlashCommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly DateTime _startTime;
    private readonly LogsRepository _logsRepository;
    
    public SlashCommandHandler(DiscordSocketClient client)
    {
        _client = client;
        _startTime = DateTime.UtcNow;
        _logsRepository = new LogsRepository();
    }
    
    public async Task HandleCommand(SocketSlashCommand command)
    {
        switch(command.Data.Name)
        {
            case "ping":
            {
                var handler = new PingCommandHandler(_client, _startTime);
                await handler.HandlePingCommand(command);
                break;   
            }
            case "find-static":
            {
                var handler = new FindCommandHandler();
                await handler.HandleFindStaticCommand(command);
                break;   
            }
            case "find-forum":
            {
                var handler = new FindCommandHandler();
                await handler.HandleFindForumCommand(command);
                break;   
            }
            case "find-nickname":
            {
                var handler = new FindCommandHandler();
                await handler.HandleFindNicknameCommand(command);
                break;   
            }
            case "top-intruder":
            {
                var handler = new TopCommandHandler();
                await handler.HandleTopIntruderCommand(command);
                break;
            }
            case "top-victim":
            {
                var handler = new TopCommandHandler();
                await handler.HandleTopVictimCommand(command);
                break;
            }
        }
        await LogCommand(command);
    }

    private async Task LogCommand(SocketSlashCommand command)
    {
        await _logsRepository.LogDataAboutChannelOrGuildAsync(command);
        
        // todo: log command and options (probably in specific handlers)
    }
}
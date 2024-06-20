using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Services.Entities.Logs;

namespace Services.Repositories;

public sealed class LogsRepository
{
    public async Task LogDataAboutChannelOrGuildAsync(SocketSlashCommand command)
    {
        // if the interaction is a REST ping interaction.
        if (command.ChannelId is null) return;

        using var context = new ApplicationDbContext();

        if (command is { Channel: SocketGuildChannel, GuildId: not null })
        {
            // if it is guild: log user, channel and guild
            await LogGuildInteraction(context, command);
        }
        else
        {
            // if it is direct message: log user and channel
            await LogDirectMessageInteraction(context, command);
        }

        await context.SaveChangesAsync();
    }

    private async Task LogGuildInteraction(ApplicationDbContext context, SocketSlashCommand command)
    {
        var guildFromDatabase = await context.LogGuilds.FirstOrDefaultAsync(x => x.Id == command.GuildId!.Value);

        if (guildFromDatabase is null)
        {
            await AddNewGuild(context, command);
        }
        // update guild maximum once a day
        else if (guildFromDatabase.ModifiedAt < DateTime.UtcNow.AddDays(-1))
        {
            await UpdateOldGuild(context, command, guildFromDatabase);
        }
    }

    private async Task AddNewGuild(ApplicationDbContext context, SocketSlashCommand command)
    {
        var socketGuildChannel = command.Channel as SocketGuildChannel;

        var guildFromCommand = new LogGuild
        {
            Id = command.GuildId!.Value,
            Name = socketGuildChannel!.Guild.Name,
            IconUrl = socketGuildChannel.Guild.IconUrl,
            ModifiedAt = DateTime.UtcNow
        };

        var channelsFromCommand = socketGuildChannel.Guild.Channels
            .Where(x => x is ITextChannel)
            .Select(x => new LogChannel
            {
                Id = x.Id,
                Name = x.Name,
                LogGuildId = x.Guild.Id,
                IsActive = true,
                Type = (int?)x.GetChannelType()
            })
            .ToList();

        var usersFromCommand = socketGuildChannel.Guild.Users
            .Select(x => new LogUser
            {
                Id = x.Id,
                Username = x.Username,
                GlobalName = x.GlobalName,
                AvatarUrl = x.GetDisplayAvatarUrl()
            })
            .ToList();

        var guildUsersFromCommand = socketGuildChannel!.Guild.Users
            .Select(x => new LogGuildUser
            {
                Id = Guid.NewGuid(),
                LogUserId = x.Id,
                LogGuildId = guildFromCommand.Id,
                GuildNickname = x.Nickname,
                JoinedAt = x.JoinedAt?.UtcDateTime
            })
            .ToList();

        context.LogGuilds.Add(guildFromCommand);
        context.LogChannels.AddRange(channelsFromCommand);

        var usersIdsFromCommand = usersFromCommand.Select(x => x.Id).ToList();

        // users from current guild in database
        var usersIdsFromDatabase = await context.LogUsers
            .Where(x => usersIdsFromCommand.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        var newUsers = usersFromCommand
            .Where(x => usersIdsFromDatabase.Contains(x.Id) == false)
            .ToList();

        // add new users from command, that are not yet in database
        context.LogUsers.AddRange(newUsers);

        // add guild users from command
        context.LogGuildUsers.AddRange(guildUsersFromCommand);
    }
    
    private async Task UpdateOldGuild(ApplicationDbContext context, SocketSlashCommand command, LogGuild guildFromDatabase)
    {
        var socketGuildChannel = command.Channel as SocketGuildChannel;

        var guildFromCommand = new LogGuild
        {
            Id = command.GuildId!.Value,
            Name = socketGuildChannel!.Guild.Name,
            IconUrl = socketGuildChannel.Guild.IconUrl,
            ModifiedAt = DateTime.UtcNow
        };

        var channelsFromCommand = socketGuildChannel.Guild.Channels
            .Where(x => x is ITextChannel)
            .Select(x => new LogChannel
            {
                Id = x.Id,
                Name = x.Name,
                LogGuildId = x.Guild.Id,
                IsActive = true,
                Type = (int?)x.GetChannelType()
            })
            .ToList();

        var usersFromCommand = socketGuildChannel.Guild.Users
            .Select(x => new LogUser
            {
                Id = x.Id,
                Username = x.Username,
                GlobalName = x.GlobalName,
                AvatarUrl = x.GetDisplayAvatarUrl()
            })
            .ToList();
        
        var guildUsersFromCommand = socketGuildChannel!.Guild.Users
            .Select(x => new LogGuildUser
            {
                Id = Guid.NewGuid(),
                LogUserId = x.Id,
                LogGuildId = guildFromCommand.Id,
                GuildNickname = x.Nickname,
                JoinedAt = x.JoinedAt
            })
            .ToList();

        var usersIdsFromCommand = usersFromCommand
            .Select(y => y.Id)
            .ToList();

        var oldUsersFromDatabase = await context.LogUsers
            .Where(x => usersIdsFromCommand.Contains(x.Id))
            .Select(x => new LogUser
            {
                Id = x.Id,
                Username = x.Username,
                GlobalName = x.GlobalName,
                AvatarUrl = x.AvatarUrl
            })
            .ToListAsync();

        // update data about old users in database
        foreach (var oldUser in oldUsersFromDatabase)
        {
            var userFromCommand = usersFromCommand.FirstOrDefault(x => x.Id == oldUser.Id);
            if (userFromCommand is null) continue;
            oldUser.Username = userFromCommand.Username;
            oldUser.GlobalName = userFromCommand.GlobalName;
            oldUser.AvatarUrl = userFromCommand.AvatarUrl;
        }
        
        // users from current guild in database
        var usersIdsFromDatabase = await context.LogGuildUsers
            .Where(x => x.LogGuildId == guildFromCommand.Id)
            .Select(x => x.LogUserId)
            .ToListAsync();

        var newUsers = usersFromCommand
            .Where(x => usersIdsFromDatabase.Contains(x.Id) == false)
            .ToList();

        context.LogUsers.AddRange(newUsers);
        
        

        var channelsFromDatabase = await context.LogChannels
            .Where(x => x.LogGuildId == guildFromCommand.Id)
            .ToListAsync();

        foreach (var channelFromDatabase in channelsFromDatabase)
        {
            if (channelsFromCommand.Any(x => x.Id == channelFromDatabase.Id) == false)
            {
                channelFromDatabase.IsActive = false;
            }
        }

        foreach (var channelFromCommand in channelsFromCommand)
        {
            var channelFromDatabase = channelsFromDatabase
                .FirstOrDefault(x => x.Id == channelFromCommand.Id);

            if (channelFromDatabase is null)
            {
                context.LogChannels.Add(channelFromCommand);
            }
            else
            {
                channelFromDatabase.Name = channelFromCommand.Name;
            }
        }


        var guildUsersFromDatabase = await context.LogGuildUsers
            .Where(x => x.LogGuildId == guildFromCommand.Id)
            .ToListAsync();

        foreach (var guildUserFromCommand in guildUsersFromCommand)
        {
            var userFromDatabase = guildUsersFromDatabase
                .FirstOrDefault(x => x.LogUserId == guildUserFromCommand.LogUserId);

            if (userFromDatabase is null)
            {
                context.LogGuildUsers.Add(guildUserFromCommand);
            }
            else
            {
                userFromDatabase.GuildNickname = guildUserFromCommand.GuildNickname;
                userFromDatabase.JoinedAt = guildUserFromCommand.JoinedAt;
            }
        }

        guildFromDatabase.Name = guildFromCommand.Name;
        guildFromDatabase.IconUrl = guildFromCommand.IconUrl;
        guildFromDatabase.ModifiedAt = guildFromCommand.ModifiedAt;
    }

    private async Task LogDirectMessageInteraction(ApplicationDbContext context, SocketSlashCommand command)
    {
        var userFromCommand = new LogUser
        {
            Id = command.User.Id,
            Username = command.User.Username,
            GlobalName = command.User.GlobalName,
            AvatarUrl = command.User.GetDisplayAvatarUrl()
        };

        var userFromDatabase = await context.LogUsers.FirstOrDefaultAsync(x => x.Id == userFromCommand.Id);
        if (userFromDatabase is null)
        {
            context.LogUsers.Add(userFromCommand);
        }
        else
        {
            userFromDatabase.GlobalName = userFromCommand.GlobalName;
            userFromDatabase.Username = userFromCommand.Username;
            userFromDatabase.AvatarUrl = userFromCommand.AvatarUrl;
        }


        var channelFromCommand = new LogChannel
        {
            Id = command.Channel.Id,
            Name = command.Channel.Name,
            LogGuildId = command.GuildId,
            IsActive = true,
            Type = (int?)command.Channel.GetChannelType()
        };

        var channelFromDatabase = await context.LogChannels.FirstOrDefaultAsync(x => x.Id == command.ChannelId!.Value);
        if (channelFromDatabase is null)
        {
            context.LogChannels.Add(channelFromCommand);
        }
        else
        {
            channelFromDatabase.Name = channelFromCommand.Name;
        }
    }
}
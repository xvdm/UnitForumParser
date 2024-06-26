﻿using Discord;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Modules.Constants;
using Modules.Handlers;
using Newtonsoft.Json;

var builder = new ConfigurationBuilder()
    .AddJsonFile("config.json", true, true);
var config = builder.Build();
var token = config["DiscordBot:Token"];

var socketConfig = new DiscordSocketConfig
{
};
var client = new DiscordSocketClient(socketConfig);
var commandHandler = new SlashCommandHandler(client);
client.Log += Log;
client.Ready += RegisterGlobalCommands;
client.SlashCommandExecuted += commandHandler.HandleCommand;
await client.LoginAsync(TokenType.Bot, token);
await client.StartAsync();

await Task.Delay(-1);
return;

async Task RegisterGlobalCommands()
{
    var globalCommands = new List<SlashCommandBuilder>
    {
        new()
        {
            Name = "ping", 
            Description = "Перевірка бота"
        },
        new()
        {
            Name = "find-static",
            Description = "Знайти скарги по статику", 
            Options = 
            [
                new SlashCommandOptionBuilder
                {
                    Name = "static", 
                    Type = ApplicationCommandOptionType.Number,
                    Description = "Статичний id гравця",
                    IsRequired = true
                }, 
                new SlashCommandOptionBuilder
                {
                    Name = "server", 
                    Type = ApplicationCommandOptionType.Number,
                    Description = "Номер серверу",
                    IsRequired = true,
                    Choices = ChoiceConstants.Servers
                }
            ]
        },
        new()
        {
            Name = "find-forum",
            Description = "Знайти скарги гравця на форумі", 
            Options = 
            [
                new SlashCommandOptionBuilder
                {
                    Name = "id", 
                    Type = ApplicationCommandOptionType.Number,
                    Description = "Id користувача на форумі",
                    IsRequired = true
                }
            ]
        },
        new()
        {
            Name = "find-nickname",
            Description = "Знайти скарги по нікнейму", 
            Options = 
            [
                new SlashCommandOptionBuilder
                {
                    Name = "nickname", 
                    Type = ApplicationCommandOptionType.String,
                    Description = "Нікнейм гравця (або його частина)",
                    IsRequired = true
                }, 
                new SlashCommandOptionBuilder
                {
                    Name = "server", 
                    Type = ApplicationCommandOptionType.Number,
                    Description = "Номер серверу",
                    IsRequired = true,
                    Choices = ChoiceConstants.Servers
                }
            ]
        },
        new()
        {
            Name = "top-intruder",
            Description = "Показати топ форумних губок", 
            Options = 
            [
                new SlashCommandOptionBuilder
                {
                    Name = "server", 
                    Type = ApplicationCommandOptionType.Number,
                    Description = "Номер серверу",
                    IsRequired = true,
                    Choices = ChoiceConstants.Servers
                }
            ]
        },
        new()
        {
            Name = "top-victim",
            Description = "Показати топ форумних бійців", 
            Options = 
            [
                new SlashCommandOptionBuilder
                {
                    Name = "server", 
                    Type = ApplicationCommandOptionType.Number,
                    Description = "Номер серверу",
                    IsRequired = true,
                    Choices = ChoiceConstants.Servers
                }
            ]
        }
    };
    
    try
    {
        var builtCommands = globalCommands
            .Select(x => x.Build())
            .Cast<ApplicationCommandProperties>()
            .ToArray();
        await client.BulkOverwriteGlobalApplicationCommandsAsync(builtCommands);
    }
    catch(HttpException exception)
    {
        var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
        Console.WriteLine(json);
    }
}

Task Log(LogMessage msg)
{
    Console.WriteLine(msg.ToString());
    return Task.CompletedTask;
}
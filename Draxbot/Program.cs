using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Discord.Addons.Interactive;
using System.Reflection;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Draxbot
{
    public class Bot
    {
        static void Main(string[] args)
        => new Bot().StartAsynq().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private string _token;
        private CommandService commands;

        // private CommandHandler _handler;

        private IServiceProvider services;

        public async Task StartAsynq()
        {
            _token = File.ReadAllText("./token.txt"); 
            _client = new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Verbose });
            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();

            services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton<InteractiveService>()
                .BuildServiceProvider();

            commands = new CommandService();
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;

            //_handler = new CommandHandler(_client);

            await Task.Delay(-1);
        }

        public async Task HandleCommandAsync(SocketMessage m)
        {
            if (!(m is SocketUserMessage msg)) return;
            if (msg.Author.IsBot) return;

            int argPos = 0;
            if (!(msg.HasStringPrefix("!", ref argPos))) return;

            var context = new SocketCommandContext(_client, msg);
            await commands.ExecuteAsync(context, argPos, services);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public static void CommandLog(string user, [Optional] string command, [Optional] string param)
        {
            if (param != null)
            {
                Console.WriteLine("{0} {1} requested {2} :: {3}", DateTime.Now, user, command, param);
            }
            else
            {
                Console.WriteLine("{0} {1} requested {2}", DateTime.Now, user, command);

            }
        }
    }
}
   

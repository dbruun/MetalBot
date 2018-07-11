using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MetalBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
       
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _service;
        

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _service = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();

            String BotToken = "NDY2Mjk4ODczMDg2NzM4NDMz.DiaDZg.lcyoY29EW5_feuRL7xHgV-ZVcbM";

            //event subscriptions
            _client.Log += Log;

            _client.UserJoined += AnnounceUserJoined;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, BotToken);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("Welcome!", $"Welcome to the NixxyPlays discord {user.Mention}");
            builder.AddInlineField("Rules", "Please take a look at the rules in #rules");
            builder.AddInlineField("Questions?", "Feel free to message a mod or the admin if you have any questions");
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
          //  guild.
            await channel.SendMessageAsync($"Welcome {user.Mention}", false, builder);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("met!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

               var result = await _commands.ExecuteAsync(context, argPos, _service);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}

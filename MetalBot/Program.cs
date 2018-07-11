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


            //Config.BotSetup is not on github to protect the bot token
            Config.BotSetup getToken = new Config.BotSetup();
            String BotToken = getToken.Token;

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
            var guild = user.Guild;
            // following while statement cycles through roles to find the mod role
            var RoleCollection = guild.Roles.GetEnumerator();
            var TargetRole = RoleCollection.Current;
         
            foreach (var R in guild.Roles)
            {
                Console.WriteLine(R);
                if (R.Permissions.DeafenMembers && !R.Permissions.Administrator && R.Permissions.ManageNicknames)
                {
                    TargetRole = R;
                    break;
                }

            }
         
            //builder creates the welcome message, mentioning the user and letting them know who the mods are
            //may want to also mention the admin 
            EmbedBuilder builder = new EmbedBuilder();
            builder.AddField("Welcome!", $"Welcome to the {guild.Name} discord {user.Mention}")
            .AddInlineField("Rules", "Please take a look at the rules in #rules")
            .AddInlineField("Questions?", $"Feel free to message one of the {TargetRole.Mention} if you have any questions")
            .AddInlineField("Most importantly", "we hope you enjoy your time here :nixLuv:");
           
            var channel = guild.DefaultChannel;
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

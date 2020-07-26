using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        //test commands, learning
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Let me sleep");
        }

        [Command("We built this city")]
        public async Task TestAsync()
        {
            await ReplyAsync("on rock and roll");
        }

        [Command("emote")]
        public async Task TestEmote()
        {
            await ReplyAsync("<:nixxypLuv:428572226447474688>");         


        }

        [Command("echo")]
        public async Task TestInputConsumption(params string[] input)
        {

            var toReturn = string.Join(' ', input);
            await ReplyAsync(toReturn);

        }

        [Command("todo")]
        public async Task GetUserToDo(params string[] input)
        {

            var toReturn = string.Join(' ', input);
            await ReplyAsync(toReturn);

        }

        [Command("add")]
        public async Task AddToToDo(params string[] input)
        {
            
        
            var toReturn = string.Join(' ', input);
            await ReplyAsync(toReturn);

        }

        [Command("callingUser")]
        public async Task test()
        {
            var user = Context.Guild.GetUser(Context.User.Id);         
            await ReplyAsync($"Useername : {user.Username}, Nickname : {user.Nickname}");
        }

        






        //test command for finding specific roles
        [Command("loop")]
        public async Task TestWelcome()
        {
            var Roles = Context.Guild.Roles;
            foreach(var R in Roles){
                if(R.Permissions.DeafenMembers && R.Permissions.ManageNicknames && !R.Permissions.Administrator)
                await ReplyAsync("" + R);
            }
        }
    }
}

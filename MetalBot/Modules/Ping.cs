using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MetalBot.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
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

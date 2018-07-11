using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MetalBot.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
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
    }
}

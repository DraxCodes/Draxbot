using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Linq;
using System.Runtime.InteropServices;

namespace Draxbot.Module
{
    public class MemeChannel : ModuleBase<SocketCommandContext>
    {
        [Command("memes")]
        [Summary("Allows access to the meme channel.")]
        public async Task MemeChannelInv([Optional] string off)
        {
            var user = Context.User as SocketGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "MemeWarrior");
            if (off != "off")
            {
                Bot.CommandLog(user.Username, "Meme Invite");
                if (user.Roles.Contains<IRole>(role))
                {
                    await Context.Channel.SendMessageAsync("You already have access to the meme channel. ＼(^ω^＼)");
                    await Context.Channel.SendMessageAsync("Please do \"!memes off\" if you want to leave the meme channel");
                }
                else
                {
                    await user.AddRoleAsync(role);
                    await Context.Channel.SendMessageAsync("You now have access to the meme channel. ＼(^ω^＼)");
                }
            } else if(user.Roles.Contains<IRole>(role))
            {
                Bot.CommandLog(user.Username, "Meme Invite Removal");
                await user.RemoveRoleAsync(role);
                await Context.Channel.SendMessageAsync("You have now removed your access to the meme channel. ＼(^ω^＼)");
            }
            else
            {
                await Context.Channel.SendMessageAsync("You don't even have access.... if you want access try !memes");
            }

        }
    }
}

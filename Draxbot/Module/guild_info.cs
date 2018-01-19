using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Draxbot.Module
{
    public class guild_info : ModuleBase <SocketCommandContext>
    {
        [Command("links")]
        [Summary("P.M's the user the various Phoenix Arising Social Media Links.")]
        public async Task info()
        {
            var user = Context.User.Username;
            Bot.CommandLog(user, "Guild Links");


            string youtube = "https://www.youtube.com/user/superhit122";
            string twitter = "https://twitter.com/PhoenixArising0";
            string armory = "http://eu.battle.net/wow/en/guild/draenor/Phoenix_Arising/";
            await Context.User.SendMessageAsync("Youtube: " + youtube);
            await Context.User.SendMessageAsync("Twitter: " + twitter);
            await Context.User.SendMessageAsync("Armory: " + armory);
            await Context.Channel.SendMessageAsync("Hey " + user + ", I sent you a Private message with the details.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.Addons.Interactive;
using System.Linq;

namespace Draxbot.Module
{
    public class EmbedNews : InteractiveBase
    {
        [Command("news", RunMode = RunMode.Async)]
        [Summary("P.M's the user the various Phoenix Arising Social Media Links.")]
        public async Task Info(string s)
        {
            TimeSpan timeOut = TimeSpan.FromSeconds(60);
            var channel = Context.Client.GetChannel(357719326905860106) as ITextChannel;
            await ReplyAsync("Enter title");
            var title = await NextMessageAsync(timeout: timeOut);
            await ReplyAsync("Enter descripiton.");
            var description = await NextMessageAsync(timeout: timeOut);
            await ReplyAsync("Enter URL.");
            var url = await NextMessageAsync(timeout: timeOut);
            await ReplyAsync("Enter IMG URL");
            var imgURL = await NextMessageAsync(timeout: timeOut);

            var builder = new EmbedBuilder();

            if (s.ToLower() == "wow")
            {
                builder.WithTitle(title.ToString())
                       .WithDescription(description.ToString())
                       .WithUrl(url.ToString())
                       .WithFooter("Powered by DraxBot & MMO Champion")
                       .WithThumbnailUrl("https://orig00.deviantart.net/65e3/f/2014/207/e/2/official_wow_icon_by_benashvili-d7sd1ab.png")
                       .WithColor(Color.Blue);
                if (imgURL.ToString().Contains("http"))
                {
                    builder.WithImageUrl(imgURL.ToString());
                }
            }
            else if (s.ToLower() == "guild")
            {
                    builder.WithTitle(title.ToString())
                    .WithDescription(description.ToString())
                    .WithUrl(url.ToString())
                    .WithFooter("Powered by DraxBot")
                    .WithThumbnailUrl("http://www.discgolfbirmingham.com/wordpress/wp-content/uploads/2014/04/phoenix-rising.jpg")
                    .WithColor(Color.DarkRed);
                if (imgURL.ToString().Contains("http"))
                {
                    builder.WithImageUrl(imgURL.ToString());
                }
            }
            else if(s.ToLower() == "youtube")
            {
                builder.WithTitle(title.ToString())
                .WithDescription(description.ToString())
                .WithUrl(url.ToString())
         
                .WithFooter("Powered by DraxBot")
                .WithThumbnailUrl("http://www.discgolfbirmingham.com/wordpress/wp-content/uploads/2014/04/phoenix-rising.jpg")
                .WithColor(Color.DarkRed);
                if (imgURL.ToString().Contains("http"))
                {
                    builder.WithImageUrl(imgURL.ToString());
                }
            }
            //await Context.Channel.SendMessageAsync("", false, builder);
            await channel.SendMessageAsync("", false, builder);
        }
    }
}

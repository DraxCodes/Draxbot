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
    [Group("news")]
    public class EmbedNews : InteractiveBase
    {
        [Command("wow", RunMode = RunMode.Async)]
        [Summary("Displays Admin defined, WoW related news as an embed.")]
        public async Task InfoWow()
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

            var builder = new EmbedBuilder()
                       .WithTitle(title.ToString())
                       .WithDescription(description.ToString())
                       .WithUrl(url.ToString())
                       .WithFooter("Powered by DraxBot & MMO Champion")
                       .WithThumbnailUrl("https://orig00.deviantart.net/65e3/f/2014/207/e/2/official_wow_icon_by_benashvili-d7sd1ab.png")
                       .WithColor(Color.Blue);

            if (imgURL.ToString().Contains("http"))
            {
                builder.WithImageUrl(imgURL.ToString());
            }

            builder.Build(); await channel.SendMessageAsync("", false, builder);
        }

        [Command("guild", RunMode = RunMode.Async)]
        [Summary("Displays Admin defined, Guild related news as an embed.")]
        public async Task InfoGuild()
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

            var builder = new EmbedBuilder()
                    .WithTitle(title.ToString())
                    .WithDescription(description.ToString())
                    .WithUrl(url.ToString())
                    .WithFooter("Powered by DraxBot")
                    .WithThumbnailUrl("http://www.discgolfbirmingham.com/wordpress/wp-content/uploads/2014/04/phoenix-rising.jpg")
                    .WithColor(Color.DarkRed);
            if (imgURL.ToString().Contains("http"))
            {
                builder.WithImageUrl(imgURL.ToString());
            }

            builder.Build(); await channel.SendMessageAsync("", false, builder);
        }

        [Command("youtube", RunMode = RunMode.Async)]
        [Summary("Displays Admin defined, Youtube related news as an embed.")]
        public async Task InfoYoutube()
        {
            TimeSpan timeOut = TimeSpan.FromSeconds(60);
            var channel = Context.Client.GetChannel(405022204024324106) as ITextChannel;
            await ReplyAsync("Enter title");
            var title = await NextMessageAsync(timeout: timeOut);
            await ReplyAsync("Enter descripiton.");
            var description = await NextMessageAsync(timeout: timeOut);
            await ReplyAsync("Enter URL.");
            var url = await NextMessageAsync(timeout: timeOut);
            await ReplyAsync("Enter IMG URL");
            var imgURL = await NextMessageAsync(timeout: timeOut);

            var builder = new EmbedBuilder()
                .WithTitle(title.ToString())
                .WithDescription(description.ToString())
                .WithUrl(url.ToString())

                .WithFooter("Powered by DraxBot")
                .WithThumbnailUrl("http://www.discgolfbirmingham.com/wordpress/wp-content/uploads/2014/04/phoenix-rising.jpg")
                .WithColor(Color.DarkRed);
            if (imgURL.ToString().Contains("http"))
            {
                builder.WithImageUrl(imgURL.ToString());
            }
            builder.Build(); await channel.SendMessageAsync("", false, builder);
        }


        [Group("raid")]
        public class EmbedNewsRaid : InteractiveBase
        {
            [Command("main", RunMode = RunMode.Async)]
            public async Task RaidEvent()
            {
                TimeSpan timeOut = TimeSpan.FromSeconds(60);
                var channel = (ITextChannel)Context.Client.GetChannel(405022204024324106);
                string guide; bool error = false;
                await ReplyAsync("Enter Raid (EN, NH, TOS, Antorus) TOV Not Supported!");
                var raid = await NextMessageAsync(timeout: timeOut);
                await ReplyAsync("Enter Date of raid (Example: Monday 22nd Jan)");
                var date = await NextMessageAsync(timeout: timeOut);
                await ReplyAsync("Enter time of raid (Example 8pm)");
                var time = await NextMessageAsync(timeout: timeOut);
                await ReplyAsync("Enter any extra Details, put ``None`` if you don't need top add extra details!");
                var details = await NextMessageAsync(timeout: timeOut);

                var legion = Context.Guild.Emotes.First(x => x.Name == "legion");

                var builder = new EmbedBuilder()
                    .WithTitle($"{legion} New Raid Event Started by {Context.User.Username}")
                    .WithDescription("All Raiders in the Main Raid Team are REQUIRED to join!")
                    .WithTimestamp(DateTime.UtcNow)
                    .WithFooter("Powered by Draxbot | Wont Display Correctly on Mobile")
                    .AddField("Raid:", "**Date:**", true);

                #region Raid Switch
                switch (raid.ToString().ToUpper())
                {
                    case "EN":
                        builder.AddField("Emerald Nightmare", $"**{date} | {time}**", true);
                        guide = "EN";
                        break;
                    case "NH":
                        builder.AddField("Nighthold", $"**{date} | {time}**", true);
                        guide = "NH";
                        break;
                    case "TOS":
                        builder.AddField("Tomb Of Sargaras", $"**{date} | {time}**", true);
                        guide = "TOS";
                        break;
                    case "ANTORUS":
                        builder.AddField("Antorus The Burning Throne", $"**{date} | {time}**", true);
                        guide = "Antorus";
                        break;
                    default:
                        guide = "none";
                        error = true;
                        break;
                }
                #endregion

                builder.AddField("Can't Make it?", "**Need Help?**", true);

                #region Link Switch
                switch (guide.ToLower())
                {
                    case "en":
                        builder.AddField("Message Draxis!", "[**Click For EN Guide**](https://www.icy-veins.com/wow/the-emerald-nightmare-raid)", true);
                        break;
                    case "nh":
                        builder.AddField("Message Draxis!", "[**Click For NH Guide**](https://www.icy-veins.com/wow/the-nighthold-raid)", true);
                        break;
                    case "tos":
                        builder.AddField("Message Draxis!", "[**Click For TOS Guide**](https://www.icy-veins.com/wow/tomb-of-sargeras-raid)", true);
                        break;
                    case "antorus":
                        builder.AddField("Message Draxis!", "[**Click For Antorus Guide**](https://www.icy-veins.com/wow/antorus-the-burning-throne-raid)", true);
                        break;
                    default:
                        builder.WithDescription("ERROR WITH INPUT");
                        error = true;
                        break;
                }
                #endregion
                #region ImageUrl Switch
                switch (raid.ToString().ToUpper())
                {
                    case "EN":
                        builder.ImageUrl = "https://orig00.deviantart.net/7595/f/2016/351/4/3/the_emerald_nightmare_by_artofty-darw2qg.jpg";
                        break;
                    case "NH":
                        builder.ImageUrl = "http://wow.zamimg.com/uploads/news/10696-nighthold-to-release-on-january-17th.jpg";
                        guide = "NH";
                        break;
                    case "TOS":
                        builder.ImageUrl = "https://raiderscdnv2-herr1437987216.netdna-ssl.com/wp-content/uploads/2016/07/160722-tomb-of-sargeras.jpg";
                        guide = "TOS";
                        break;
                    case "ANTORUS":
                        builder.ImageUrl = "https://www.icy-veins.com/forums/news/33936-antorus-the-burning-throne-releases-on-nov-28.jpg";
                        guide = "Antorus";
                        break;
                    default:
                        error = true;
                        break;
                }
                #endregion

                if (details.ToString().ToLower() != "none")
                {
                    builder.AddField("Extra Details", $"{details}");
                }

                if (error)
                {

                    await ReplyAsync("Sorry there was an error with your input");
                }
                else
                {
                    builder.Build(); await channel.SendMessageAsync("", false, builder);
                }
            }
            [Command("social", RunMode = RunMode.Async)]
            public async Task SocialRaidEvent()
            {
                TimeSpan timeOut = TimeSpan.FromSeconds(60);
                var channel = (ITextChannel)Context.Client.GetChannel(405022204024324106);
                string guide; bool error = false;
                await ReplyAsync("Enter Raid (EN, NH, TOS, Antorus) TOV Not Supported!");
                var raid = await NextMessageAsync(timeout: timeOut);
                await ReplyAsync("Enter Date of raid (Example: Monday 22nd Jan)");
                var date = await NextMessageAsync(timeout: timeOut);
                await ReplyAsync("Enter time of raid (Example 8pm)");
                var time = await NextMessageAsync(timeout: timeOut);
                await ReplyAsync("Enter any extra Details, put ``None`` if you don't need top add extra details!");
                var details = await NextMessageAsync(timeout: timeOut);
                var legion = Context.Guild.Emotes.First(x => x.Name == "legion");

                var builder = new EmbedBuilder()
                    .WithTitle($"{legion} New Raid Event Started by {Context.User.Username}")
                    .WithDescription($"No requirement to join, Just let {Context.User.Username} know you are wanting to join!")
                    .WithTimestamp(DateTime.UtcNow)
                    .WithFooter("Powered by Draxbot | Wont Display Correctly on Mobile")
                    .AddField("Raid:", "**Date:**", true);

                #region Raid Switch
                switch (raid.ToString().ToUpper())
                {
                    case "EN":
                        builder.AddField("Emerald Nightmare", $"**{date} | {time}**", true);
                        guide = "EN";
                        break;
                    case "NH":
                        builder.AddField("Nighthold", $"**{date} | {time}**", true);
                        guide = "NH";
                        break;
                    case "TOS":
                        builder.AddField("Tomb Of Sargaras", $"**{date} | {time}**", true);
                        guide = "TOS";
                        break;
                    case "ANTORUS":
                        builder.AddField("Antorus The Burning Throne", $"**{date} | {time}**", true);
                        guide = "Antorus";
                        break;
                    default:
                        guide = "none";
                        error = true;
                        break;
                }
                #endregion

                builder.AddField("Can't Make it?", "**Need Help?**", true);

                #region Link Switch
                switch (guide.ToLower())
                {
                    case "en":
                        builder.AddField("Message Draxis!", "[**Click For EN Guide**](https://www.icy-veins.com/wow/the-emerald-nightmare-raid)", true);
                        break;
                    case "nh":
                        builder.AddField("Message Draxis!", "[**Click For NH Guide**](https://www.icy-veins.com/wow/the-nighthold-raid)", true);
                        break;
                    case "tos":
                        builder.AddField("Message Draxis!", "[**Click For TOS Guide**](https://www.icy-veins.com/wow/tomb-of-sargeras-raid)", true);
                        break;
                    case "antorus":
                        builder.AddField("Message Draxis!", "[**Click For Antorus Guide**](https://www.icy-veins.com/wow/antorus-the-burning-throne-raid)", true);
                        break;
                    default:
                        builder.WithDescription("ERROR WITH INPUT");
                        error = true;
                        break;
                }
                #endregion
                #region ImageUrl Switch
                switch (raid.ToString().ToUpper())
                {
                    case "EN":
                        builder.ImageUrl = "https://orig00.deviantart.net/7595/f/2016/351/4/3/the_emerald_nightmare_by_artofty-darw2qg.jpg";
                        break;
                    case "NH":
                        builder.ImageUrl = "http://wow.zamimg.com/uploads/news/10696-nighthold-to-release-on-january-17th.jpg";
                        guide = "NH";
                        break;
                    case "TOS":
                        builder.ImageUrl = "https://raiderscdnv2-herr1437987216.netdna-ssl.com/wp-content/uploads/2016/07/160722-tomb-of-sargeras.jpg";
                        guide = "TOS";
                        break;
                    case "ANTORUS":
                        builder.ImageUrl = "https://www.icy-veins.com/forums/news/33936-antorus-the-burning-throne-releases-on-nov-28.jpg";
                        guide = "Antorus";
                        break;
                    default:
                        error = true;
                        break;
                }
                #endregion

                if (details.ToString().ToLower() != "none")
                {
                    builder.AddField("Extra Details", $"{details}");
                }

                if (error)
                {

                    await ReplyAsync("Sorry there was an error with your input");
                }
                else
                {
                    builder.Build(); await channel.SendMessageAsync("", false, builder);
                }
            }
        }
    }
}





using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.InteropServices;
using System;
using Newtonsoft.Json;
using System.Net;
using RaiderIO_Library;
using System.Linq;
using Discord.WebSocket;

namespace Draxbot.Module
{
    public class MythicPlusCompleted : ModuleBase<SocketCommandContext>
    {
        [Command("mcheck")]
        [Summary("Checks if a user still needs a +10/+15 Mythic Plus Dungeon for the week.")]
        public async Task MyhticPlusComletion(string s)
        {
            var user = Context.User as SocketGuildUser;
            var officer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Officer");
            var GM = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Guild Master");
            Bot.CommandLog(user.Username, "Mythic Plus Checker");

            if (user.Roles.Contains<IRole>(officer) || user.Roles.Contains<IRole>(GM))
            {
                string url = ($"https://raider.io/api/v1/characters/profile?region=eu&realm=draenor&name={s}&fields=mythic_plus_weekly_highest_level_runs");
                string jsonData = null;
                jsonData = new WebClient().DownloadString(url);
                Character c = JsonConvert.DeserializeObject<WeeklyBestMythicPlus>(jsonData);

                if (c.WeeklyBestMythicPlus.Length <= 1)
                {
                    var builder = new EmbedBuilder();
                    builder.WithTitle($"{c.Name} Weekly Mythic+ Completed")
                           .WithDescription($"{c.Name} Has not completed any Mythic+ this week. If this data is not correct and you " +
                                            $"have done a mythic+ then please let an officer know. Raider.IO only shows Mythic+ completed if it's available on the Blizzard Leader Boards.")
                           .WithThumbnailUrl("https://i.gyazo.com/8cb62339d27480a330a63804fcb09810.png")
                           .WithFooter($"Powered by DraxBot & Raider.IO | {DateTime.Now.ToString("MMMM dd, yyyy")}")
                            .WithUrl(c.RaiderIO_URL);
                    await Context.Channel.SendMessageAsync("", false, builder);
                }
                else
                {
                    var builder = new EmbedBuilder();
                    builder.WithTitle($"{c.Name} Weekly Mythic+ Completed")
                           .WithThumbnailUrl("https://i.gyazo.com/8cb62339d27480a330a63804fcb09810.png")
                           .WithFooter($"Powered by DraxBot & Raider.IO | {DateTime.Now.ToString("MMMM dd, yyyy")}")
                           .WithUrl(c.RaiderIO_URL);
                    foreach (var x in c.WeeklyBestMythicPlus)
                    {
                        builder.AddInlineField(x.Dungeon, $"+{x.Level} | Completed: {x.Date.ToString("MMMM dd, yyyy")}");
                    }
                    await Context.Channel.SendMessageAsync("", false, builder);
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("You do not have the required roll to use this command!");
            }
        }
    }
}

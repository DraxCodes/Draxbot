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
    public class MythicPlus : ModuleBase<SocketCommandContext>
    {
        [Command("mplus")]
        [Summary("Finds the characters WOW Stats.")]
        public async Task _MythicPlus(string s, [Optional] string server)
        { 
            if (server == null) server = "draenor";
            var user = Context.User as SocketGuildUser;
            Bot.CommandLog(user.Username, $"Mythic+ Stats for {s}");

            string jsonDataUrl = ($"https://raider.io/api/v1/characters/profile?region=eu&realm={server}&name={s}&fields=mythic_plus_highest_level_runs%2Cgear%2Cguild%2Cmythic_plus_scores%2Cmythic_plus_ranks%2Cmythic_plus_recent_runs%2Cmythic_plus_best_runs%2Cmythic_plus_weekly_highest_level_runs%2Cprevious_mythic_plus_scores");
            string jsonData = null;
            jsonData = new WebClient().DownloadString(jsonDataUrl);
            Character currentInfo = JsonConvert.DeserializeObject<Character>(jsonData);

            /*  var builder = new EmbedBuilder()
                  .WithTitle($"{currentInfo.Name} | Mythic+ Breakdown")
                  .AddField("Current Season", "")
                  .WithColor(Color.Gold)
                  .WithUrl(currentInfo.RaiderIO_URL)
                  .WithColor(Color.DarkOrange)
                  .WithFooter($"Powered by DraxBot & Raider.IO | {DateTime.Now.ToString("MMMM dd, yyyy")}"); */

            foreach (var d in currentInfo.HighestEverMythicPlus)
            {
                await Context.Channel.SendMessageAsync($"{d.dungeon} Level: {d.mythic_level}");
            }
            
        }

        private static string DownloadJsonData(string url)
        {
            var jsonData = new WebClient().DownloadString(url);
            return jsonData;
        }
    }
}

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
            Character mPlusData = JsonConvert.DeserializeObject<Character>(jsonData);

            var builderMP = new EmbedBuilder() { Title = $"Mythic+ Data For {s}", /*Description = $"Mythic+ Data Up To Date As Of {DateTime.UtcNow}",*/ Url = mPlusData.RaiderIO_URL};
            builderMP.WithFooter($"Powered by Draxbot & Raider.IO | {DateTime.UtcNow}");

            foreach (var d in mPlusData.HighestEverMythicPlus)
            {
                builderMP.AddField($"[{d.short_name}]({d.url}) +{d.mythic_level}", null , true);
            }

            builderMP.Build(); await ReplyAsync("", false, builderMP);
        }

        private static string DownloadJsonData(string url)
        {
            var jsonData = new WebClient().DownloadString(url);
            return jsonData;
        }
    }
}

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
    [Group("mplus")]
    public class MythicPlus : ModuleBase<SocketCommandContext>
    {
        [Command("highest")]
        [Summary("Finds the characters WOW Stats.")]
        public async Task Highest_MythicPlus(string s, [Optional] string server)
        { 
            if (server == null) server = "draenor";
            var user = Context.User as SocketGuildUser;
            Bot.CommandLog(user.Username, $"Mythic+ highest for {s}");
            await Context.Channel.TriggerTypingAsync();
            var mPlusData = MplusData(s, server);

            var builderMP = new EmbedBuilder() { Title = $"Highest Mythic+ Data For {mPlusData.Name} | {mPlusData.Race} {mPlusData.SpecName} {mPlusData.Class} | {mPlusData.CharGear.EquipedILVL} ILVL", Url = mPlusData.RaiderIO_URL }
                .WithFooter($"Powered by Draxbot & Raider.IO | {DateTime.UtcNow}")
                .WithThumbnailUrl(mPlusData.ThumbnailURL)
                .AddField($"Current Rating", mPlusData.CurrentMythicPlusScore.all.ToString() , true);


            foreach (var d in mPlusData.HighestEverMythicPlus)
            {
                builderMP.AddField($"{d.short_name} +{d.mythic_level}", $"[+{d.num_keystone_upgrades} Chest | Link]({d.url})" , true);
            }

            builderMP.Build(); await ReplyAsync("", false, builderMP);
        }

        [Command("weekly")]
        [Summary("Finds and displays users top weekly runs.")]
        public async Task Weekly_MythicPlus(string s, [Optional] string server)
        {
            if (server == null) server = "draenor";
            var user = Context.User as SocketGuildUser;
            Bot.CommandLog(user.Username, $"Mythic+ weekly for {s}");
            await Context.Channel.TriggerTypingAsync();
            var mPlusData = MplusData(s, server);

            var builderMP = new EmbedBuilder() { Title = $"Weekly Mythic+ Data For {mPlusData.Name} | {mPlusData.Race} {mPlusData.SpecName} {mPlusData.Class} | {mPlusData.CharGear.EquipedILVL} ILVL", Url = mPlusData.RaiderIO_URL }
                .WithFooter($"Powered by Draxbot & Raider.IO | {DateTime.UtcNow}")
                .WithThumbnailUrl(mPlusData.ThumbnailURL)
                .AddField($"Current Rating", mPlusData.CurrentMythicPlusScore.all.ToString(), true);

            foreach (var d in mPlusData.WeeklyBestMythicPlus)
            {
                builderMP.AddField($"{d.Shortname} +{d.Level}", $"[+{d.KeystoneUpgrade} Chest | Link]({d.URL})", true);
            }

            builderMP.Build(); await ReplyAsync("", false, builderMP);
        }

        private static Character MplusData(string s, [Optional] string server)
        {
            string jsonDataUrl = ($"https://raider.io/api/v1/characters/profile?region=eu&realm={server}&name={s}&fields=mythic_plus_highest_level_runs%2Cgear%2Cguild%2Cmythic_plus_scores%2Cmythic_plus_ranks%2Cmythic_plus_recent_runs%2Cmythic_plus_best_runs%2Cmythic_plus_weekly_highest_level_runs%2Cprevious_mythic_plus_scores");
            string jsonData = null;
            jsonData = new WebClient().DownloadString(jsonDataUrl);
            Character mPlusData = JsonConvert.DeserializeObject<Character>(jsonData);
            return mPlusData;
        }

        [Command("affix", RunMode = RunMode.Async)]
        public async Task Affixes()
        {
            var affixData = Afixes("eu");
            var replyBuilder = new EmbedBuilder()
                .WithTitle("This Weeks Mythic+ Affixes")
                .WithDescription($"[{affixData.affix_details[0].name}]({affixData.affix_details[0].wowhead_url}) | [{affixData.affix_details[1].name}]({affixData.affix_details[1].wowhead_url}) | [{affixData.affix_details[2].name}]({affixData.affix_details[2].wowhead_url})")
                .WithFooter($"Powered by Draxbot & Raider.IO", "https://media.forgecdn.net/avatars/117/23/636399071197048271.png")
                .WithTimestamp(DateTime.UtcNow)
                .WithThumbnailUrl("https://i.imgur.com/EVMAxGc.png")
                .AddField(affixData.affix_details[0].name, $"{affixData.affix_details[0].description}")
                .AddField(affixData.affix_details[1].name, $"{affixData.affix_details[1].description}")
                .AddField(affixData.affix_details[2].name, $"{affixData.affix_details[2].description}");
            replyBuilder.Build(); await ReplyAsync("", false, replyBuilder);
        }

        private static Afixes Afixes(string region)
        {
            string jsonDataUrl = ($"https://raider.io/api/v1/mythic-plus/affixes?region={region}");
            string jsonData = null;
            jsonData = new WebClient().DownloadString(jsonDataUrl);
            Afixes AffixData = JsonConvert.DeserializeObject<Afixes>(jsonData);
            return AffixData;
        }
    }
}

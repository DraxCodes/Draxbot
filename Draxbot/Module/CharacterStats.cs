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
    public class CharacterStats : ModuleBase<SocketCommandContext>
    {
        [Command("wowstats"), Summary("Finds the characters WOW Stats.")]
        public async Task CharStats(string s, [Optional] string server)
        {
            if (server == null) server = "draenor";
            try
            {
                var user = Context.User as SocketGuildUser;
                Bot.CommandLog(user.Username, $"WoW Stats for {s}");
                string charURL = ($"https://raider.io/api/v1/characters/profile?region=eu&realm={server}&name={s}&fields=gear%2Cguild%2Craid_progression%2Cmythic_plus_scores%2Cprevious_mythic_plus_scores");
                string jsonDataChar = null;
                jsonDataChar = new WebClient().DownloadString(charURL);
                Character c = JsonConvert.DeserializeObject<Character>(jsonDataChar);

                var gender = (Char.ToUpperInvariant(c.Gender[0]) + c.Gender.Substring(1));
                var builder = new EmbedBuilder()
                    .WithTitle($"{c.Name} {c.Realm} {c.Region.ToUpper()} | Character Info")
                    .WithThumbnailUrl($"{c.ThumbnailURL}")
                    .AddInlineField("Name & Guild", $"{c.Name} | {c.Guild.name}")
                    .AddInlineField("Links", $"[RaiderIO]({c.RaiderIO_URL}) | [Armory](https://worldofwarcraft.com/en-gb/character/{server}/{s}/)")
                    .AddInlineField("Class", $"{c.SpecName} {c.Class}")
                    .AddInlineField("Race", $"{gender} {c.Race}")
                    .AddInlineField("Equiped ILVL", c.CharGear.EquipedILVL)
                    .AddInlineField("Total ILVL", c.CharGear.TotalILVL)
                    .AddInlineField("Artifact Trait", c.CharGear.APTraits)
                    .AddInlineField("Emerald Nightmare", c.RaidProg.EmeraldNightmare.summary)
                    .AddInlineField("Trial Of Valor", c.RaidProg.TrialOfValor.summary)
                    .AddInlineField("The Nighthold", c.RaidProg.Nighthold.summary)
                    .AddInlineField("Tomb Of Sargeras", c.RaidProg.TombOfSargeras.summary)
                    .AddInlineField("Antorus The Burning Throne", c.RaidProg.Antorus.summary)
                    .AddField("Mythic+", $"Below is the mythic+ data for {c.Name} for the current seaon and last season.")
                    .AddInlineField("Current Season Overall", c.CurrentMythicPlusScore.all)
                    .AddInlineField("Last Seasons Mythic+ Overall", c.PastMythicPlusScore.all)
                    .WithUrl(c.RaiderIO_URL)
                    .WithColor(Color.DarkOrange)
                    .WithFooter($"Powered by DraxBot & Raider.IO | {DateTime.Now.ToString("MMMM dd, yyyy")}");

                await Context.Channel.SendMessageAsync("", false, builder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw;
            }
        }
    }
}

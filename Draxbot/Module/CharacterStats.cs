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
using LiteDB;
using System.Text;

namespace Draxbot.Module
{
    [Group("stats")]
    public class CharacterStats : ModuleBase<SocketCommandContext>
    {
        [Command("wow"), Summary("Finds the characters WOW Stats.")]
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
        
        [Group("ow")]
        public class OverwatchStats : ModuleBase<SocketCommandContext>
        {
            [Command(), Summary("Retrieve user overwatch Stats")]
            public async Task OWStats(string s = null)
            {
                {
                    Bot.CommandLog(Context.User.Username, $"WoW Stats for {s}");
                    Context.Guild.Emotes.First(x => x.Name == "rank1");

                    var bronze = new Color(140, 120, 83);
                    var silver = new Color(230, 232, 250);
                    var diamond = new Color(238, 252, 255);


                    var charName = new StringBuilder(s);
                    var btag = charName.Replace("#", "-").ToString();
                    await Context.Channel.TriggerTypingAsync();

                    if (s == null)
                    {
                        using (var db = new LiteDatabase(@"OWData.db"))
                        {
                            var saveData = db.GetCollection<OWSaveData>("saveData");
                            var data = saveData.Find(x => x.Name == Context.User.Username);
                            foreach (var d in data)
                            {
                                if (d.Name == Context.User.Username)
                                {
                                    btag = d.Btag;
                                }
                                else
                                {
                                    break;
                                }
                                break;
                            }
                        }
                    }

                    string apiLink = ($"http://ow-api.herokuapp.com/profile/pc/us/{btag}");
                    Console.WriteLine($"{Context.User.Username} Requested OW Stats : {apiLink}");
                    var jsonDataStats = new WebClient().DownloadString(apiLink);
                    OWStats stats = JsonConvert.DeserializeObject<OWStats>(jsonDataStats);
                    var rank = stats.competitive.rank;



                    #region rank variables
                    var rank1 = Context.Guild.Emotes.First(x => x.Name == "rank1");
                    var rank2 = Context.Guild.Emotes.First(x => x.Name == "rank2");
                    var rank3 = Context.Guild.Emotes.First(x => x.Name == "rank3");
                    var rank4 = Context.Guild.Emotes.First(x => x.Name == "rank4");
                    var rank5 = Context.Guild.Emotes.First(x => x.Name == "rank5");
                    var rank6 = Context.Guild.Emotes.First(x => x.Name == "rank6");
                    var rank7 = Context.Guild.Emotes.First(x => x.Name == "rank7");
                    #endregion


                    var builder = new EmbedBuilder()
                         .WithFooter("Powered by Draxbot & OW-API", "https://image.ibb.co/mhcU6m/Disocrd_OWIcon.png")
                         .WithTimestamp(DateTime.UtcNow)
                         .WithUrl($"https://masteroverwatch.com/profile/pc/global/{btag}")
                         .AddField("Competitive Playtime", stats.playtime.competitive.ToString(), true)
                         .AddField("Competitive Rank ", $"{stats.competitive.rank}", true)
                         .AddField("Played ", stats.games.competitive.played.ToString(), true)
                         .AddField("Wins ", stats.games.competitive.won.ToString(), true)
                         .AddField("Losses ", stats.games.competitive.lost.ToString(), true)
                         .AddField("Draws ", stats.games.competitive.draw.ToString(), true);
                    if (stats.portrait == null)
                    {
                        builder.WithThumbnailUrl("https://overwatch-shop.jp/wp-content/uploads/2016/09/Overwatch_circle_logo.svg_.png");
                    }
                    else
                    {
                        builder.WithThumbnailUrl(stats.portrait);
                    }

                    #region Embed Title (Ranks)

                    if (rank <= 1499)
                    {
                        builder.Title = ($"{rank1} OverWatch Stats for {btag}");
                    }
                    else if (rank >= 1500 && rank <= 1999)
                    {
                        builder.WithTitle($"{rank2} OverWatch Stats for {btag}");
                    }
                    else if (rank >= 2000 && rank <= 2499)
                    {
                        builder.WithTitle($"{rank3} OverWatch Stats for {btag}");
                    }
                    else if (rank >= 2500 && rank <= 2999)
                    {
                        builder.WithTitle($"{rank4} OverWatch Stats for {btag}");
                    }
                    else if (rank >= 3000 && rank <= 3499)
                    {
                        builder.WithTitle($"{rank5} OverWatch Stats for {btag}");
                    }
                    else if (rank >= 3500 && rank <= 3999)
                    {
                        builder.WithTitle($"{rank6} OverWatch Stats for {btag}");
                    }
                    else if (rank >= 4000)
                    {
                        builder.WithTitle($"{rank7} OverWatch Stats for {btag}");
                    }
                    #endregion

                    #region Embed Color Ranks
                    if (rank <= 1499)
                    {
                        builder.Color = bronze;
                    }
                    else if (rank >= 1500 && rank <= 1999)
                    {
                        builder.Color = silver;
                    }
                    else if (rank >= 2000 && rank <= 2499)
                    {
                        builder.Color = Color.Gold;
                    }
                    else if (rank >= 2500)
                    {
                        builder.Color = diamond;
                    }
                    #endregion

                    builder.Build();
                    await Context.Channel.SendMessageAsync("", false, builder);
                }
            }
            [Command("save"), Summary("Saves users battletag")]
            public async Task OWSave(string s)
            {
                var charName = new StringBuilder(s);
                charName.Replace("#", "-");
                var discordName = Context.User.Username;
                await Context.Channel.TriggerTypingAsync();

                using (var db = new LiteDatabase(@"OWData.db"))
                {
                    var saveData = db.GetCollection<OWSaveData>("saveData");
                    var userCheck = saveData.Find(Query.Where("Name", name => name.AsString.Length > 0));
                    saveData.EnsureIndex(x => x.Name);

                    var checker = saveData.Count(Query.EQ("Name", discordName));

                    if (checker < 1)
                    {
                        var Input = new OWSaveData()
                        {
                            Name = discordName,
                            Btag = charName.ToString()
                        };

                        saveData.Insert(Input);

                        await Context.Channel.SendMessageAsync($"All set {discordName}, your btag is now saved! In future just use ``!stats ow`` to get your stats.");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"Hey {discordName}, It seems you already added your battletag so you can just use ``!stats ow`` If this is incorect please contact Draxis :)");
                    }
                }
            }
        }

    }
}
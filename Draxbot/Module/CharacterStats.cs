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
            [Command(RunMode = RunMode.Async), Summary("Retrieve user overwatch Stats"),]
            public async Task OWStats(string s = null)
            {
                Bot.CommandLog(Context.User.Username, $"OW Stats for {s}");
                var bronze = new Color(69, 10, 10);
                var silver = new Color(230, 232, 250);
                var diamond = new Color(238, 252, 255);
                OWProfile stats;


                #region rank variables
                var rank1 = Context.Guild.Emotes.First(x => x.Name == "rank1");
                var rank2 = Context.Guild.Emotes.First(x => x.Name == "rank2");
                var rank3 = Context.Guild.Emotes.First(x => x.Name == "rank3");
                var rank4 = Context.Guild.Emotes.First(x => x.Name == "rank4");
                var rank5 = Context.Guild.Emotes.First(x => x.Name == "rank5");
                var rank6 = Context.Guild.Emotes.First(x => x.Name == "rank6");
                var rank7 = Context.Guild.Emotes.First(x => x.Name == "rank7");
                #endregion

                var charName = new StringBuilder(s);
                var btag = charName.Replace("#", "-").ToString();
                await Context.Channel.TriggerTypingAsync();

                if (s == null)
                {
                    using (var db = new LiteDatabase(@"OWData.db"))
                    {
                        var saveData = db.GetCollection<OWSaveData>("saveData");
                        var count = saveData.Count(Query.EQ("Name", Context.User.Id.ToString()));

                        if (count <= 0)
                        {
                            var Failbuilder = new EmbedBuilder()
                                .WithTitle("ERROR")
                                .WithFooter("Powered by Draxbot & OW-API", "https://image.ibb.co/mhcU6m/Disocrd_OWIcon.png")
                                .WithDescription("There seems to be an issue. I can't find your BattleTag in my Database. Please use the command ``!stats save [Battletag]`` before you use anymore Overwatch Stat Commands.")
                                .WithTimestamp(DateTime.UtcNow);
                            Failbuilder.Build(); await ReplyAsync("", false, Failbuilder);
                        }

                        var data = saveData.Find(x => x.Name == Context.User.Id.ToString());


                        foreach (var d in data)
                        {
                            if (d.Name == Context.User.Id.ToString())
                            {
                                btag = d.Btag;
                            }
                        }
                    }

                    stats = Get_Stats(btag);
                    int.TryParse(stats.rating, out int rank);
                    int losses = stats.competitiveStats.games.played - stats.competitiveStats.games.won;


                    var builder = new EmbedBuilder()
                         .WithFooter("Powered by Draxbot & OW-API", "https://image.ibb.co/mhcU6m/Disocrd_OWIcon.png")
                         .WithDescription("For more detailed information regarding competitive play, Please try ``!stats ow hero [hero name]``")
                         .WithThumbnailUrl(stats.icon)
                         .WithTimestamp(DateTime.UtcNow)
                         .WithUrl($"https://masteroverwatch.com/profile/pc/global/{btag}")
                         .AddField("Competitive Rank ", $"{rank}", true)
                         .AddField("Played ", $"{stats.competitiveStats.games.played}", true)
                         .AddField("Wins ", $"{stats.competitiveStats.games.won}", true)
                         .AddField("Losses ", $"{losses}", true);

                    #region Embed Title (Ranks)

                    if (rank <= 1499)
                    {
                        builder.Title = ($"{rank1} OverWatch Stats for {stats.name}");
                    }
                    else if (rank >= 1500 && rank <= 1999)
                    {
                        builder.WithTitle($"{rank2} OverWatch Stats for {stats.name}");
                    }
                    else if (rank >= 2000 && rank <= 2499)
                    {
                        builder.WithTitle($"{rank3} OverWatch Stats for {stats.name}");
                    }
                    else if (rank >= 2500 && rank <= 2999)
                    {
                        builder.WithTitle($"{rank4} OverWatch Stats for {stats.name}");
                    }
                    else if (rank >= 3000 && rank <= 3499)
                    {
                        builder.WithTitle($"{rank5} OverWatch Stats for {stats.name}");
                    }
                    else if (rank >= 3500 && rank <= 3999)
                    {
                        builder.WithTitle($"{rank6} OverWatch Stats for {stats.name}");
                    }
                    else if (rank >= 4000)
                    {
                        builder.WithTitle($"{rank7} OverWatch Stats for {stats.name}");
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

                    builder.Build(); await ReplyAsync("", false, builder);
                }
            }
            [Command("hero", RunMode = RunMode.Async), Summary("Gets overwatch hero data for specific user.")]
            public async Task OwStatsChampion(string champion = null)
            {
                await Context.Channel.TriggerTypingAsync();
                if (champion == null)
                {
                    var Failbuilder = new EmbedBuilder()
                               .WithTitle("ERROR")
                               .WithFooter("Powered by Draxbot & OW-API", "https://image.ibb.co/mhcU6m/Disocrd_OWIcon.png")
                               .WithDescription("There seems to be an issue. You didn't supply a valid champion name! Please do the ``!champions`` command find a list of champions. Correct format for command is ``!stats ow hero [Hero Name]``")
                               .WithTimestamp(DateTime.UtcNow);
                    Failbuilder.Build(); await ReplyAsync("", false, Failbuilder);
                }
                champion = champion.ToLower();

                if (champion == "dva")
                {
                    champion = "dVa";
                }

                Bot.CommandLog(Context.User.Username, $"OW Stats for {Context.User.Username}");
                var bronze = new Color(69, 10, 10);
                var silver = new Color(230, 232, 250);
                var diamond = new Color(238, 252, 255);
                var builder = new EmbedBuilder();
                #region rank variables
                var rank1 = Context.Guild.Emotes.First(x => x.Name == "rank1");
                var rank2 = Context.Guild.Emotes.First(x => x.Name == "rank2");
                var rank3 = Context.Guild.Emotes.First(x => x.Name == "rank3");
                var rank4 = Context.Guild.Emotes.First(x => x.Name == "rank4");
                var rank5 = Context.Guild.Emotes.First(x => x.Name == "rank5");
                var rank6 = Context.Guild.Emotes.First(x => x.Name == "rank6");
                var rank7 = Context.Guild.Emotes.First(x => x.Name == "rank7");
                #endregion
                string btag = null;
                using (var db = new LiteDatabase(@"OWData.db"))
                {
                    var saveData = db.GetCollection<OWSaveData>("saveData");
                    var count = saveData.Count(Query.EQ("Name", Context.User.Id.ToString()));

                    if (count <= 0)
                    {
                        var Failbuilder = new EmbedBuilder()
                            .WithTitle("ERROR")
                            .WithFooter("Powered by Draxbot & OW-API", "https://image.ibb.co/mhcU6m/Disocrd_OWIcon.png")
                            .WithDescription("There seems to be an issue. I can't find your BattleTag in my Database. Please use the command ``!stats save [Battletag]`` before you use anymore Overwatch Stat Commands.")
                            .WithTimestamp(DateTime.UtcNow);
                        Failbuilder.Build(); await ReplyAsync("", false, Failbuilder);
                    }

                    var data = saveData.Find(x => x.Name == Context.User.Id.ToString());


                    foreach (var d in data)
                    {
                        if (d.Name == Context.User.Id.ToString())
                        {
                            btag = d.Btag;
                        }
                    }



                    OWProfile stats = GetChampionStats(btag, champion);
                    stats = Get_Stats(btag);
                    int.TryParse(stats.rating, out int rank);

                    Career SelectedChampion = null;
                    #region Probably a really bad way of doing this
                    switch (champion.ToLower())
                    {
                        case "ana":
                            SelectedChampion = stats.competitiveStats.careerStats.Ana;
                            break;
                        case "bastion":
                            SelectedChampion = stats.competitiveStats.careerStats.Bastion;
                            break;
                        case "dva":
                            SelectedChampion = stats.competitiveStats.careerStats.DVA;
                            break;
                        case "doomfist":
                            SelectedChampion = stats.competitiveStats.careerStats.Doomfist;
                            break;
                        case "genji":
                            SelectedChampion = stats.competitiveStats.careerStats.Genji;
                            break;
                        case "hanzo":
                            SelectedChampion = stats.competitiveStats.careerStats.Hanzo;
                            break;
                        case "junkrat":
                            SelectedChampion = stats.competitiveStats.careerStats.Junkrat;
                            break;
                        case "lucio":
                            SelectedChampion = stats.competitiveStats.careerStats.Lucio;
                            break;
                        case "mccree":
                            SelectedChampion = stats.competitiveStats.careerStats.Mccree;
                            break;
                        case "mei":
                            SelectedChampion = stats.competitiveStats.careerStats.Mei;
                            break;
                        case "mercy":
                            SelectedChampion = stats.competitiveStats.careerStats.Mercy;
                            break;
                        case "moira":
                            SelectedChampion = stats.competitiveStats.careerStats.Moira;
                            break;
                        case "orisa":
                            SelectedChampion = stats.competitiveStats.careerStats.Orisa;
                            break;
                        case "pharah":
                            SelectedChampion = stats.competitiveStats.careerStats.Pharah;
                            break;
                        case "reaper":
                            SelectedChampion = stats.competitiveStats.careerStats.Reaper;
                            break;
                        case "reinhardt":
                            SelectedChampion = stats.competitiveStats.careerStats.Reinhardt;
                            break;
                        case "rein":
                            SelectedChampion = stats.competitiveStats.careerStats.Reinhardt;
                            break;
                        case "roadhog":
                            SelectedChampion = stats.competitiveStats.careerStats.Roadhog;
                            break;
                        case "soldier76":
                            SelectedChampion = stats.competitiveStats.careerStats.Soldier76;
                            break;
                        case "soldier":
                            SelectedChampion = stats.competitiveStats.careerStats.Soldier76;
                            break;
                        case "sombra":
                            SelectedChampion = stats.competitiveStats.careerStats.Sombra;
                            break;
                        case "symmetra":
                            SelectedChampion = stats.competitiveStats.careerStats.Symmetra;
                            break;
                        case "torbjorn":
                            SelectedChampion = stats.competitiveStats.careerStats.Torbjorn;
                            break;
                        case "torb":
                            SelectedChampion = stats.competitiveStats.careerStats.Torbjorn;
                            break;
                        case "tracer":
                            SelectedChampion = stats.competitiveStats.careerStats.Tracer;
                            break;
                        case "widowmaker":
                            SelectedChampion = stats.competitiveStats.careerStats.Widowmaker;
                            break;
                        case "widow":
                            SelectedChampion = stats.competitiveStats.careerStats.Widowmaker;
                            break;
                        case "winston":
                            SelectedChampion = stats.competitiveStats.careerStats.Winston;
                            break;
                        case "zarya":
                            SelectedChampion = stats.competitiveStats.careerStats.Zarya;
                            break;
                        case "zenyatta":
                            SelectedChampion = stats.competitiveStats.careerStats.Zenyatta;
                            break;
                        case "zen":
                            SelectedChampion = stats.competitiveStats.careerStats.Zenyatta;
                            break;
                        default:
                            break;
                    }

                    #endregion  

                    //Convert User Champion Input to CamelCase
                    champion = Char.ToUpperInvariant(champion[0]) + champion.Substring(1);
                    //calculate losses
                    int losses = SelectedChampion.game.gamesPlayed - SelectedChampion.game.gamesWon - SelectedChampion.game.gamesTied;

                    var dataBuilder = new EmbedBuilder()
                         .WithDescription("Below are the best results you have had in competitive in a single game. This is not average, this is best :)")
                         .WithFooter("Powered by Draxbot & OW-API", "https://image.ibb.co/mhcU6m/Disocrd_OWIcon.png")
                         .WithThumbnailUrl(stats.icon)
                         .WithTimestamp(DateTime.UtcNow)
                         .WithUrl($"https://masteroverwatch.com/profile/pc/global/{btag}")
                         .AddField("Competitive Rank ", $"{rank}", true)
                         .AddField("Playtime & Win %", $"{SelectedChampion.game.timePlayed} | {SelectedChampion.game.winPercentage}", true)
                         .AddField("Wins", SelectedChampion.game.gamesWon.ToString(), true)
                         .AddField("Losses", losses.ToString(), true);



                    #region Embed Champion Specific Extras

                    switch (champion.ToLower())
                    {
                        case "ana":
                            
                            break;
                        case "bastion":
                            
                            break;
                        case "dva":
                            
                            break;
                        case "doomfist":
                            
                            break;
                        case "genji":
                            
                            break;
                        case "hanzo":
                            
                            break;
                        case "junkrat":
                            
                            break;
                        case "lucio":
                            
                            break;
                        case "mccree":
                            
                            break;
                        case "mei":
                            
                            break;
                        case "mercy":
                            dataBuilder.AddField("Blaster Kills", SelectedChampion.heroSpecific.blasterKills.ToString(), true)
                                .AddField("Damage Buffed", SelectedChampion.heroSpecific.damageAmplifiedMostInGame.ToString(), true)
                                .AddField("Healing Done", SelectedChampion.assists.healingDoneMostInGame.ToString(), true)
                                .AddField("Defensive Assists", SelectedChampion.assists.defensiveAssistsMostInGame.ToString(), true)
                                .AddField("Offensive Assists", SelectedChampion.assists.offensiveAssistsMostInGame.ToString(), true)
                                .AddField("Players Resurrected", SelectedChampion.heroSpecific.playersResurrectedMostInGame.ToString(), true);
                            break;
                        case "moira":
                            
                            break;
                        case "orisa":
                            
                            break;
                        case "pharah":
                            
                            break;
                        case "reaper":
                            
                            break;
                        case "reinhardt":
                            
                            break;
                        case "rein":
                            
                            break;
                        case "roadhog":
                            
                            break;
                        case "soldier76":
                            
                            break;
                        case "soldier":
                            
                            break;
                        case "sombra":
                            
                            break;
                        case "symmetra":
                            
                            break;
                        case "torbjorn":
                            
                            break;
                        case "torb":

                            break;
                        case "tracer":

                            break;
                        case "widowmaker":

                            break;
                        case "widow":

                            break;
                        case "winston":

                            break;
                        case "zarya":

                            break;
                        case "zenyatta":

                            break;
                        case "zen":

                            break;
                        default:
                            break;
                    }

                    #endregion

                    #region Embed Title (Ranks)

                    if (rank <= 1499)
                    {
                        dataBuilder.Title = ($"{rank1} Overwatch Hero Stats For {stats.name} On {champion}");
                    }
                    else if (rank >= 1500 && rank <= 1999)
                    {
                        dataBuilder.WithTitle($"{rank2} Overwatch Hero Stats For {stats.name} On {champion}");
                    }
                    else if (rank >= 2000 && rank <= 2499)
                    {
                        dataBuilder.WithTitle($"{rank3} Overwatch Hero Stats For {stats.name} On {champion}");
                    }
                    else if (rank >= 2500 && rank <= 2999)
                    {
                        dataBuilder.WithTitle($"{rank4} Overwatch Hero Stats For {stats.name} On {champion}");
                    }
                    else if (rank >= 3000 && rank <= 3499)
                    {
                        dataBuilder.WithTitle($"{rank5} Overwatch Hero Stats For {stats.name} On {champion}");
                    }
                    else if (rank >= 3500 && rank <= 3999)
                    {
                        dataBuilder.WithTitle($"{rank6} Overwatch Hero Stats For {stats.name} On {champion}");
                    }
                    else if (rank >= 4000)
                    {
                        dataBuilder.WithTitle($"{rank7} Overwatch Hero Stats For {stats.name} On {champion}");
                    }
                    #endregion

                    #region Embed Color Ranks
                    if (rank <= 1499)
                    {
                        dataBuilder.Color = bronze;
                    }
                    else if (rank >= 1500 && rank <= 1999)
                    {
                        dataBuilder.Color = silver;
                    }
                    else if (rank >= 2000 && rank <= 2499)
                    {
                        dataBuilder.Color = Color.Gold;
                    }
                    else if (rank >= 2500)
                    {
                        dataBuilder.Color = diamond;
                    }
                    #endregion

                    await ReplyAsync("", false, dataBuilder);
                }

            }
        }

        private static OWProfile GetChampionStats(string btag, string champion)
        {
            string apiLink = ($"https://ow-api.com/v1/stats/pc/eu/{btag}/heroes/{champion}");
            var jsonDataStats = new WebClient().DownloadString(apiLink);
            OWProfile stats = JsonConvert.DeserializeObject<OWProfile>(jsonDataStats);
            return stats;
        }

        private static OWProfile Get_Stats(string btag)
        {
            string apiLink = ($"https://ow-api.com/v1/stats/pc/eu/{btag}/complete");
            var jsonDataStats = new WebClient().DownloadString(apiLink);
            OWProfile stats = JsonConvert.DeserializeObject<OWProfile>(jsonDataStats);
            return stats;
        }

        #region OW SAVE
        [Command("save"), Summary("Saves users battletag")]
        public async Task OWSave(string s)
        {
            var charName = new StringBuilder(s);
            charName.Replace("#", "-");
            var discordName = Context.User.Id.ToString();
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
                        Name = discordName.ToString(),
                        Btag = charName.ToString()
                    };

                    saveData.Insert(Input);

                    await Context.Channel.SendMessageAsync($"All set {Context.User.Username}, your btag is now saved! In future just use ``!stats ow`` to get your stats.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"Hey {Context.User.Username}, It seems you already added your battletag so you can just use ``!stats ow`` If this is incorect please contact Draxis :)");
                }
            }
        }
        #endregion
    }

}
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Draxbot.Module
{
    public class GuideWOW : ModuleBase<SocketCommandContext>
    {
        [Command("guide", RunMode = RunMode.Async)]
        public async Task WowGuide(string _spec = null, string _class = null)
        {
            if (_spec == null || _class == null)
            {
                var Failbuilder = new EmbedBuilder()
                                  .WithTitle("ERROR")
                                  .WithFooter("Powered by Draxbot & OW-API", "https://image.ibb.co/mhcU6m/Disocrd_OWIcon.png")
                                  .WithDescription("There seems to be an issue with your command ``!guide``. Please ensure you are using it correcty: ``!guide [spec] [class]``")
                                  .WithTimestamp(DateTime.UtcNow);
                Failbuilder.Build(); await ReplyAsync("", false, Failbuilder);
            }

            switch (_spec)
            {
                case "enhance":
                    _spec = "enhancement";
                    break;
                case "prot":
                    _spec = "protection";
                    break;
                case "boomy":
                    _spec = "balance";
                    break;
                case "kitty":
                    _spec = "feral";
                    break;
                case "destro":
                    _class = "destruction";
                    break;
                case "aff":
                    _class = "affliction";
                    break;
                case "demo":
                    _class = "demonology";
                    break;
                case "sub":
                    _class = "subtlety";
                    break;
                case "ass":
                    _class = "assassination";
                    break;
                case "bm":
                    _class = "beastmastery";
                    break;
                case "mm":
                    _class = "marksmanship";
                    break;
                case "bear":
                    _class = "guardian";
                    break;
                case "resto":
                    _class = "restoration";
                    break;
                case "ele":
                    _class = "elemental";
                    break;
                default:
                    break;
            }
            switch (_class.ToLower())
            {
                case "dk":
                    _class = "deathknight";
                    break;
                case "dh":
                    _class = "demonhunter";
                    break;
                case "pala":
                    _class = "paladin";
                    break;
                default:
                    break;
            }

            Bot.CommandLog(Context.User.Username, "guide", $"");
            await Context.Channel.TriggerTypingAsync();
            var replyBuilder = new EmbedBuilder();

            try
            {
                WowGuide guideInfo = JsonConvert.DeserializeObject<WowGuide>(File.ReadAllText("guides.json"));
                Data results = null;
                string tierToken = null;


                #region Duplicate Checking
                if (_spec.ToLower() == "holy" && _class.ToLower() == "paladin")
                {
                    results = guideInfo.classes.paladin.specs.Holy;
                    tierToken = guideInfo.classes.paladin.tier_token;
                }
                if (_spec.ToLower() == "holy" && _class.ToLower() == "priest")
                {
                    results = guideInfo.classes.priest.specs.Holy;
                    tierToken = guideInfo.classes.priest.tier_token;
                }
                if (_spec.ToLower() == "protection" && _class.ToLower() == "paladin")
                {
                    results = guideInfo.classes.paladin.specs.Protection;
                    tierToken = guideInfo.classes.paladin.tier_token;
                }
                if (_spec.ToLower() == "protection" && _class.ToLower() == "warrior")
                {
                    results = guideInfo.classes.Warrior.specs.Protection;
                    tierToken = guideInfo.classes.Warrior.tier_token;
                }
                if (_spec.ToLower() == "frost" && _class.ToLower() == "mage")
                {
                    results = guideInfo.classes.mage.specs.Frost;
                    tierToken = guideInfo.classes.mage.tier_token;
                }
                if (_spec.ToLower() == "frost" && _class.ToLower() == "deathknight")
                {
                    results = guideInfo.classes.deathknight.specs.Frost;
                    tierToken = guideInfo.classes.deathknight.tier_token;
                }
                if (_spec.ToLower() == "restoration" && _class.ToLower() == "druid")
                {
                    results = guideInfo.classes.druid.specs.Restoration;
                    tierToken = guideInfo.classes.druid.tier_token;
                }
                if (_spec.ToLower() == "restoration" && _class.ToLower() == "shaman")
                {
                    results = guideInfo.classes.shaman.specs.Restoration;
                    tierToken = guideInfo.classes.shaman.tier_token;
                }
                #endregion

                #region Switch _spec
                switch (_spec.ToLower())
                {
                    case "enhancement":
                        results = guideInfo.classes.shaman.specs.Enhacement;
                        tierToken = guideInfo.classes.shaman.tier_token;
                        break;
                    case "elemental":
                        results = guideInfo.classes.shaman.specs.Elemental;
                        tierToken = guideInfo.classes.shaman.tier_token;
                        break;
                    case "unholy":
                        results = guideInfo.classes.deathknight.specs.Unholy;
                        tierToken = guideInfo.classes.deathknight.tier_token;
                        break;
                    case "blood":
                        results = guideInfo.classes.deathknight.specs.Blood;
                        tierToken = guideInfo.classes.deathknight.tier_token;
                        break;
                    case "hevoc":
                        results = guideInfo.classes.demonhunter.specs.Havoc;
                        tierToken = guideInfo.classes.demonhunter.tier_token;
                        break;
                    case "vengence":
                        results = guideInfo.classes.demonhunter.specs.Vengence;
                        tierToken = guideInfo.classes.demonhunter.tier_token;
                        break;
                    case "balance":
                        results = guideInfo.classes.druid.specs.Ballance;
                        tierToken = guideInfo.classes.druid.tier_token;
                        break;
                    case "feral":
                        results = guideInfo.classes.druid.specs.Feral;
                        tierToken = guideInfo.classes.druid.tier_token;
                        break;
                    case "guardian":
                        results = guideInfo.classes.druid.specs.Guardian;
                        tierToken = guideInfo.classes.druid.tier_token;
                        break;
                    case "beastmastery":
                        results = guideInfo.classes.hunter.specs.Beast_Mastery;
                        tierToken = guideInfo.classes.hunter.tier_token;
                        break;
                    case "marksmanship":
                        results = guideInfo.classes.hunter.specs.Marksmanship;
                        tierToken = guideInfo.classes.hunter.tier_token;
                        break;
                    case "survival":
                        results = guideInfo.classes.hunter.specs.Survival;
                        tierToken = guideInfo.classes.hunter.tier_token;
                        break;
                    case "Fire":
                        results = guideInfo.classes.mage.specs.Fire;
                        tierToken = guideInfo.classes.mage.tier_token;
                        break;
                    case "arcane":
                        results = guideInfo.classes.mage.specs.Arcane;
                        tierToken = guideInfo.classes.mage.tier_token;
                        break;
                    case "brewmaster":
                        results = guideInfo.classes.monk.specs.Brewmaster;
                        tierToken = guideInfo.classes.monk.tier_token;
                        break;
                    case "mistweaver":
                        results = guideInfo.classes.monk.specs.Mistweaver;
                        tierToken = guideInfo.classes.monk.tier_token;
                        break;
                    case "windwalker":
                        results = guideInfo.classes.monk.specs.Windwalker;
                        tierToken = guideInfo.classes.monk.tier_token;
                        break;
                    case "ret":
                        results = guideInfo.classes.paladin.specs.Ret;
                        tierToken = guideInfo.classes.paladin.tier_token;
                        break;
                    case "disc":
                        results = guideInfo.classes.priest.specs.Disc;
                        tierToken = guideInfo.classes.priest.tier_token;
                        break;
                    case "shadow":
                        results = guideInfo.classes.priest.specs.Shadow;
                        tierToken = guideInfo.classes.priest.tier_token;
                        break;
                    case "subtlety":
                        results = guideInfo.classes.rogue.specs.Subtlety;
                        tierToken = guideInfo.classes.rogue.tier_token;
                        break;
                    case "assassination":
                        results = guideInfo.classes.rogue.specs.Assassination;
                        tierToken = guideInfo.classes.rogue.tier_token;
                        break;
                    case "outlaw":
                        results = guideInfo.classes.rogue.specs.Outlaw;
                        tierToken = guideInfo.classes.rogue.tier_token;
                        break;
                    case "demonology":
                        results = guideInfo.classes.warlock.specs.Demonology;
                        tierToken = guideInfo.classes.warlock.tier_token;
                        break;
                    case "affliction":
                        results = guideInfo.classes.warlock.specs.Affliction;
                        tierToken = guideInfo.classes.warlock.tier_token;
                        break;
                    case "destruction":
                        results = guideInfo.classes.warlock.specs.Destruction;
                        tierToken = guideInfo.classes.warlock.tier_token;
                        break;
                    case "arms":
                        results = guideInfo.classes.Warrior.specs.Arms;
                        tierToken = guideInfo.classes.Warrior.tier_token;
                        break;
                    case "fury":
                        results = guideInfo.classes.Warrior.specs.Fury;
                        tierToken = guideInfo.classes.Warrior.tier_token;
                        break;
                    default:
                        break;
                }
                #endregion

                if (results == null || tierToken == null)
                {
                    var Failbuilder = new EmbedBuilder()
                                   .WithTitle("ERROR")
                                   .WithFooter("Powered by Draxbot & OW-API", "https://image.ibb.co/mhcU6m/Disocrd_OWIcon.png")
                                   .WithDescription("There seems to be an issue with your command ``!guide``. Please ensure you are using it correcty: ``!guide [spec] [class]``")
                                   .WithTimestamp(DateTime.UtcNow);
                    Failbuilder.Build(); await ReplyAsync("", false, Failbuilder);
                }

                replyBuilder.WithTimestamp(DateTime.UtcNow)
                    .WithColor(Color.Magenta)
                    .WithTitle(results.Title)
                    .WithDescription(results.Description)
                    .WithUrl(results.Link)
                    .WithFooter("Powered By DraxBot & Icy Veins | See a problem? PM Draxis")
                    .AddField("Stat Priority", $"{results.Stats}")
                    .AddField("Shares Tier Tokens With", tierToken)
                    .AddField("Links", $"[Link To Icy Veins Guide]({results.Link})")
                    .WithThumbnailUrl("https://cdn2.iconfinder.com/data/icons/basic-office-snippets/170/Basic_Office-9-512.png");

                replyBuilder.Build(); await ReplyAsync("", false, replyBuilder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw;
            }
            


            
        }
    }
}

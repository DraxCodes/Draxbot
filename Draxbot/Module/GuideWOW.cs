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
                                  .WithFooter("Powered by Draxbot")
                                  .WithDescription("There seems to be an issue with your command ``!guide``. Please ensure you are using it correcty: ``!guide [spec] [class]``")
                                  .WithTimestamp(DateTime.UtcNow);
                Failbuilder.Build(); await ReplyAsync("", false, Failbuilder);
            }
            string checked_spec = WOWGuide_Spec.Check(_spec.ToLower());
            string checked_class = WOWGuide_Class.Check(_class.ToLower());

            Bot.CommandLog(Context.User.Username, "guide", $"");
            await Context.Channel.TriggerTypingAsync();
            var replyBuilder = new EmbedBuilder();

            try
            {
                WOWGuide.ParseData(checked_spec, checked_class);
                var results = WOWGuide_Result.Results;
                var tierToken = WOWGuide_Result.Token;

             

                if (results == null || tierToken == null)
                {
                    var Failbuilder = new EmbedBuilder()
                                   .WithTitle("ERROR")
                                   .WithFooter("Powered by Draxbot")
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
                    .AddField("Note", "Remember these are only basic stat priorities, you should ALWAYS Sim yourself if possible too!")
                    .AddField("Links", $"[Link To Icy Veins Guide]({results.Link}) | [Link To RaidBot, Sim yourself!](https://www.raidbots.com/)")
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

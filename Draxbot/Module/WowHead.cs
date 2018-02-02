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
using System.IO;

namespace Draxbot.Module
{
    public class WowHead : ModuleBase<SocketCommandContext>
    {
        [Command("wowhead", RunMode = RunMode.Async)]
        public async Task WowHeadSearch([Remainder]string s = null)
        {
            await Context.Channel.TriggerTypingAsync();
            if (s == null)
            {
                var Failbuilder = new EmbedBuilder()
                               .WithTitle("ERROR")
                               .WithFooter("Powered by Draxbot & Wowhead", "http://www.theabandonedones.com/images/wowhead-1.png")
                               .WithDescription("There seems to be an issue with your command ``!wowhead``. Please ensure you are using it correcty: ``!wowhead [search]``")
                               .WithTimestamp(DateTime.UtcNow);
                Failbuilder.Build(); await ReplyAsync("", false, Failbuilder);
            }

            try
            {
                string _ApiKey = File.ReadAllText("./googleAPI.txt");
                string customSearchEngineID = "009576280758054807232:n2zhxdvclwe";
                var search = WowSearch(s.ToString(), _ApiKey, customSearchEngineID);
                var replyBuilder = new EmbedBuilder()
                    .WithFooter("Powered by Draxbot & Wowhead", "http://www.theabandonedones.com/images/wowhead-1.png")
                    .WithTimestamp(DateTime.UtcNow);

                

                foreach (var d in search.items.Take(1))
                {
                    var sBuilderSnippet = new StringBuilder(d.snippet).Replace('\n', ' ');
                    replyBuilder
                        .WithTitle($"{d.title}")
                        .WithDescription($"{sBuilderSnippet}")
                        .WithUrl($"{d.link}");
                    foreach (var item in d.pagemap.cse_thumbnail)
                    {
                        replyBuilder.WithThumbnailUrl(item.src);
                    }

                }

                replyBuilder.Build(); await ReplyAsync("", false, replyBuilder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        public static WowHeadSearch WowSearch(string s, string apikey, string ID)
        {
            string apiLink = ($"https://www.googleapis.com/customsearch/v1?key={apikey}&cx={ID}&q={s}");
            var jsonDataStats = new WebClient().DownloadString(apiLink);
            WowHeadSearch search = JsonConvert.DeserializeObject<WowHeadSearch>(jsonDataStats);
            return search;
        }
    }
}

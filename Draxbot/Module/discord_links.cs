using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.InteropServices;
using System;

namespace Draxbot.Module
{
    public class Repeat : ModuleBase<SocketCommandContext>
    {
        //discord command (links to all the various discords when comand is triggered!
        [Command("discord")]
        [Summary("Links the requested discord invite link.")]
        public async Task discord_links(string s, [Optional] string s2)
        {
            var user = Context.User.Username;
            if (s2 == null) { s = s.ToLower(); }
            else { s = s.ToLower() + " " + s2.ToLower(); }
            Bot.CommandLog(user, "Discord Link", s);

            try
            {
                bool check = false; 
                XmlDocument doc = new XmlDocument();
                doc.Load("class_discord.xml");

                foreach (XmlNode node in doc.DocumentElement)
                {
                    string name = node.Attributes[0].InnerText;
                    if (name == s)
                    {
                        check = true;
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            await Context.Channel.SendMessageAsync("Hey " + user + ", this is the " + s + " class discord: " + child.InnerText);
                        }
                    }
                }
                if (check == false)
                {
                    Bot.CommandLog(user, "Link was not available.", s);
                    await Context.Channel.SendMessageAsync($"Hey {user} that discord link ({s}) doesn't seem to be available. Please report this to Draxis if you think this is wrong.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            } 

        }
    }
}

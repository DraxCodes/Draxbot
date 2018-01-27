using System;
using System.Collections.Generic;
using System.Text;

namespace Draxbot
{
    public class Afixes
    {
        public string region { get; set; }
        public string title { get; set; }
        public string leaderboard_url { get; set; }
        public Affix_Details[] affix_details { get; set; }
    }

    public class Affix_Details
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string wowhead_url { get; set; }
    }
}

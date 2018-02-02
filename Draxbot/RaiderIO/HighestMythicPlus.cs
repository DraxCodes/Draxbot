using System;
using System.Collections.Generic;
using System.Text;

namespace RaiderIO_Lib
{
    public class Mythic_Plus_Highest_Level_Runs
    {
        public string dungeon { get; set; }
        public string short_name { get; set; }
        public int mythic_level { get; set; }
        public DateTime completed_at { get; set; }
        public int clear_time_ms { get; set; }
        public int num_keystone_upgrades { get; set; }
        public int score { get; set; }
        public string url { get; set; }
    }

}

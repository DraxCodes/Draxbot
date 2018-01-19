using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RaiderIO_Library
{
    public class WeeklyBestMythicPlus : Character
    {
        [JsonProperty("dungeon")]
        public string Dungeon { get; set; }
        [JsonProperty("short_name")]
        public string Shortname { get; set; }
        [JsonProperty("mythic_level")]
        public int Level { get; set; }
        [JsonProperty("completed_at")]
        public DateTime Date { get; set; }
        [JsonProperty("clear_time_ms")]
        public int ClearTime { get; set; }
        [JsonProperty("num_keystone_upgrades")]
        public int KeystoneUpgrade { get; set; }
        [JsonProperty("url")]
        public string URL { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RaiderIO_Library
{
    public class Raid_Progression
    {
        [JsonProperty("the-emerald-nightmare")]
        public TheEmeraldNightmare EmeraldNightmare { get; set; }

        [JsonProperty("trial-of-valor")]
        public TrialOfValor TrialOfValor { get; set; }

        [JsonProperty("the-nighthold")]
        public TheNighthold Nighthold { get; set; }

        [JsonProperty("tomb-of-sargeras")]
        public TombOfSargeras TombOfSargeras { get; set; }

        [JsonProperty("antorus-the-burning-throne")]
        public AntorusBurningThrone Antorus { get; set; }
    }

    public class TheEmeraldNightmare
    {
        public string summary { get; set; }
        public int total_bosses { get; set; }
        public int normal_bosses_killed { get; set; }
        public int heroic_bosses_killed { get; set; }
        public int mythic_bosses_killed { get; set; }
    }

    public class TrialOfValor
    {
        public string summary { get; set; }
        public int total_bosses { get; set; }
        public int normal_bosses_killed { get; set; }
        public int heroic_bosses_killed { get; set; }
        public int mythic_bosses_killed { get; set; }
    }

    public class TheNighthold
    {
        public string summary { get; set; }
        public int total_bosses { get; set; }
        public int normal_bosses_killed { get; set; }
        public int heroic_bosses_killed { get; set; }
        public int mythic_bosses_killed { get; set; }
    }

    public class TombOfSargeras
    {
        public string summary { get; set; }
        public int total_bosses { get; set; }
        public int normal_bosses_killed { get; set; }
        public int heroic_bosses_killed { get; set; }
        public int mythic_bosses_killed { get; set; }
    }

    public class AntorusBurningThrone
    {
        public string summary { get; set; }
        public int total_bosses { get; set; }
        public int normal_bosses_killed { get; set; }
        public int heroic_bosses_killed { get; set; }
        public int mythic_bosses_killed { get; set; }
    }

}

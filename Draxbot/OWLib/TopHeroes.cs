using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Draxbot
{
    public class Topheroes
    {
        [JsonProperty("ana")]
        public Ana Ana { get; set; }

        [JsonProperty("bastion")]
        public Bastion Bastion { get; set; }

        [JsonProperty("dVa")]
        public Dva DVA { get; set; }

        [JsonProperty("doomfist")]
        public Doomfist Doomfist { get; set; }

        [JsonProperty("genji")]
        public Genji Genji { get; set; }

        [JsonProperty("hanzo")]
        public Hanzo Hanzo { get; set; }

        [JsonProperty("junkrat")]
        public Junkrat Junkrat { get; set; }

        [JsonProperty("lucio")]
        public Lucio Lucio { get; set; }

        [JsonProperty("mccree")]
        public Mccree Mccree { get; set; }

        [JsonProperty("mei")]
        public Mei Mei { get; set; }

        [JsonProperty("mercy")]
        public Mercy Mercy { get; set; }

        [JsonProperty("moira")]
        public Moira Moira { get; set; }

        [JsonProperty("orisa")]
        public Orisa Orisa { get; set; }

        [JsonProperty("pharah")]
        public Pharah Pharah { get; set; }

        [JsonProperty("reaper")]
        public Reaper Reaper { get; set; }

        [JsonProperty("reinhardt")]
        public Reinhardt Reinhardt { get; set; }

        [JsonProperty("roadhog")]
        public Roadhog Roadhog { get; set; }

        [JsonProperty("soldier76")]
        public Soldier76 Soldier76 { get; set; }

        [JsonProperty("sombra")]
        public Sombra Sombra { get; set; }

        [JsonProperty("symmetra")]
        public Symmetra Symmetra { get; set; }

        [JsonProperty("torbjorn")]
        public Torbjorn Torbjorn { get; set; }

        [JsonProperty("tracer")]
        public Tracer Tracer { get; set; }

        [JsonProperty("widowmaker")]
        public Widowmaker Widowmaker { get; set; }

        [JsonProperty("winston")]
        public Winston Winston { get; set; }

        [JsonProperty("zarya")]
        public Zarya Zarya { get; set; }

        [JsonProperty("zenyatta")]
        public Zenyatta Zenyatta { get; set; }
    }

    public class Ana : HeroStats { }
    public class Bastion : HeroStats { }
    public class Dva : HeroStats { }
    public class Doomfist : HeroStats { }
    public class Genji : HeroStats { }
    public class Hanzo : HeroStats { }
    public class Junkrat : HeroStats { }
    public class Lucio : HeroStats { }
    public class Mccree : HeroStats { }
    public class Mei : HeroStats { }
    public class Mercy : HeroStats { }
    public class Moira : HeroStats { }
    public class Orisa : HeroStats { }
    public class Pharah : HeroStats { }
    public class Reaper : HeroStats { }
    public class Reinhardt : HeroStats { }
    public class Roadhog : HeroStats { }
    public class Soldier76 : HeroStats { }
    public class Sombra : HeroStats { }
    public class Symmetra : HeroStats { }
    public class Torbjorn : HeroStats { }
    public class Tracer : HeroStats { }
    public class Widowmaker : HeroStats { }
    public class Winston : HeroStats { }
    public class Zarya : HeroStats { }
    public class Zenyatta : HeroStats{ }

    public class HeroStats
    {
        public string timePlayed { get; set; }
        public int gamesWon { get; set; }
        public int winPercentage { get; set; }
        public int weaponAccuracy { get; set; }
        public float eliminationsPerLife { get; set; }
        public int multiKillBest { get; set; }
        public int objectiveKillsAvg { get; set; }
    }
}

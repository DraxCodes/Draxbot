using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Draxbot
{

    public class OWProfile
    {
        public string icon { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public string levelIcon { get; set; }
        public int prestige { get; set; }
        public string prestigeIcon { get; set; }
        public string rating { get; set; }
        public string ratingIcon { get; set; }
        public string ratingName { get; set; }
        public int gamesWon { get; set; }
        public Competitivestats competitiveStats { get; set; }

    }

    public class Competitivestats
    {
        public int eliminationsAvg { get; set; }
        public int damageDoneAvg { get; set; }
        public int deathsAvg { get; set; }
        public int finalBlowsAvg { get; set; }
        public int healingDoneAvg { get; set; }
        public int objectiveKillsAvg { get; set; }
        public string objectiveTimeAvg { get; set; }
        public int soloKillsAvg { get; set; }
        public Games games { get; set; }
        public Awards awards { get; set; }
        public Topheroes topHeroes { get; set; }
        public Careerstats careerStats { get; set; }
    }

    public class Games
    {
        public int played { get; set; }
        public int won { get; set; }
    }

    public class Awards
    {
        public int cards { get; set; }
        public int medals { get; set; }
        public int medalsBronze { get; set; }
        public int medalsSilver { get; set; }
        public int medalsGold { get; set; }
    }

}

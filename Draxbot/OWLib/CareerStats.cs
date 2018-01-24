using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Draxbot
{
    public class Careerstats
    {
        [JsonProperty("ana")]
        public Career Ana { get; set; }

        [JsonProperty("bastion")]
        public Career Bastion { get; set; }

        [JsonProperty("dVa")]
        public Career DVA { get; set; }

        [JsonProperty("doomfist")]
        public Career Doomfist { get; set; }

        [JsonProperty("genji")]
        public Career Genji { get; set; }

        [JsonProperty("hanzo")]
        public Career Hanzo { get; set; }

        [JsonProperty("junkrat")]
        public Career Junkrat { get; set; }

        [JsonProperty("lucio")]
        public Career Lucio { get; set; }

        [JsonProperty("mccree")]
        public Career Mccree { get; set; }

        [JsonProperty("mei")]
        public Career Mei { get; set; }

        [JsonProperty("mercy")]
        public Career Mercy { get; set; }

        [JsonProperty("moira")]
        public Career Moira { get; set; }

        [JsonProperty("orisa")]
        public Career Orisa { get; set; }

        [JsonProperty("pharah")]
        public Career Pharah { get; set; }

        [JsonProperty("reaper")]
        public Career Reaper { get; set; }

        [JsonProperty("reinhardt")]
        public Career Reinhardt { get; set; }

        [JsonProperty("roadhog")]
        public Career Roadhog { get; set; }

        [JsonProperty("soldier76")]
        public Career Soldier76 { get; set; }

        [JsonProperty("sombra")]
        public Career Sombra { get; set; }

        [JsonProperty("symmetra")]
        public Career Symmetra { get; set; }

        [JsonProperty("torbjorn")]
        public Career Torbjorn { get; set; }

        [JsonProperty("tracer")]
        public Career Tracer { get; set; }

        [JsonProperty("widowmaker")]
        public Career Widowmaker { get; set; }

        [JsonProperty("winston")]
        public Career Winston { get; set; }

        [JsonProperty("zarya")]
        public Career Zarya { get; set; }

        [JsonProperty("zenyatta")]
        public Career Zenyatta { get; set; }
    }

    public class Career
    {
        public Assists assists { get; set; }
        public Average average { get; set; }
        public Best best { get; set; }
        public Combat combat { get; set; }
        public Deaths deaths { get; set; }
        public Herospecific heroSpecific { get; set; }
        public Game game { get; set; }
        public Matchawards matchAwards { get; set; }
    }

    public class Assists
    {
        public int defensiveAssists { get; set; }
        public int defensiveAssistsMostInGame { get; set; }
        public int healingDone { get; set; }
        public int healingDoneMostInGame { get; set; }
        public int offensiveAssists { get; set; }
        public int offensiveAssistsMostInGame { get; set; }
        public int turretsDestroyed { get; set; }
    }

    public class Average
    {
        public float allDamageDone { get; set; }
        public int barrierDamageDone { get; set; }
        public int criticalHits { get; set; }
        public int deaths { get; set; }
        public int defensiveAssists { get; set; }
        public int eliminations { get; set; }
        public float eliminationsPerLife { get; set; }
        public int finalBlows { get; set; }
        public int healingDone { get; set; }
        public int heroDamageDone { get; set; }
        public int meleeFinalBlows { get; set; }
        public int objectiveKills { get; set; }
        public int objectiveTime { get; set; }
        public int offensiveAssists { get; set; }
        public int selfHealing { get; set; }
        public int soloKills { get; set; }
        public int soundBarriersProvided { get; set; }
        public int timeSpentOnFire { get; set; }
    }

    public class Best
    {
        public int allDamageDoneMostInGame { get; set; }
        public int allDamageDoneMostInLife { get; set; }
        public int barrierDamageDoneMostInGame { get; set; }
        public int criticalHitsMostInGame { get; set; }
        public int criticalHitsMostInLife { get; set; }
        public int eliminationsMostInGame { get; set; }
        public int eliminationsMostInLife { get; set; }
        public int finalBlowsMostInGame { get; set; }
        public int heroDamageDoneMostInGame { get; set; }
        public int heroDamageDoneMostInLife { get; set; }
        public int killsStreakBest { get; set; }
        public int meleeFinalBlowsMostInGame { get; set; }
        public int objectiveKillsMostInGame { get; set; }
        public string objectiveTimeMostInGame { get; set; }
        public int soloKillsMostInGame { get; set; }
        public int timeSpentOnFireMostInGame { get; set; }
        public string weaponAccuracyBestInGame { get; set; }
    }

    public class Combat
    {
        public int barrierDamageDone { get; set; }
        public int criticalHits { get; set; }
        public string criticalHitsAccuracy { get; set; }
        public int damageDone { get; set; }
        public int deaths { get; set; }
        public int eliminations { get; set; }
        public int environmentalKills { get; set; }
        public int finalBlows { get; set; }
        public int heroDamageDone { get; set; }
        public int meleeFinalBlows { get; set; }
        public int objectiveKills { get; set; }
        public string objectiveTime { get; set; }
        public int quickMeleeAccuracy { get; set; }
        public int soloKills { get; set; }
        public string timeSpentOnFire { get; set; }
        public string weaponAccuracy { get; set; }
    }

    public class Deaths
    {
        public int deaths { get; set; }
    }

    public class Herospecific
    {
        public int selfHealing { get; set; }
        public int selfHealingMostInGame { get; set; }
        public int soundBarriersProvided { get; set; }
        public int soundBarriersProvidedMostInGame { get; set; }
        public int blasterKills { get; set; }
        public int blasterKillsMostInGame { get; set; }
        public int damageAmplified { get; set; }
        public int damageAmplifiedMostInGame { get; set; }
        public int playersResurrected { get; set; }
        public int playersResurrectedMostInGame { get; set; }
    }

    public class Game
    {
        public int gamesPlayed { get; set; }
        public int gamesTied { get; set; }
        public int gamesWon { get; set; }
        public string timePlayed { get; set; }
        public string winPercentage { get; set; }
        public int CountPluralOneGameOtherGamesLost { get; set; }
    }

    public class Matchawards
    {
        public int cards { get; set; }
        public int medals { get; set; }
        public int medalsBronze { get; set; }
        public int medalsGold { get; set; }
        public int medalsSilver { get; set; }
    }
}

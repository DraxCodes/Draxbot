using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RaiderIO_Lib;

namespace RaiderIO_Library
{
    /// <summary>
    /// Gets character info from Raider.io
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Character Name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Character Race.
        /// </summary>
        [JsonProperty("race")]
        public string Race { get; set; }

        /// <summary>
        /// Character Class.
        /// </summary>
        [JsonProperty("class")]
        public string Class { get; set; }

        /// <summary>
        /// Current Active Spec
        /// </summary>
        [JsonProperty("active_spec_name")]
        public string SpecName { get; set; }

        /// <summary>
        /// Current Spec.
        /// </summary>
        [JsonProperty("active_spec_role")]
        public string ActiveRole { get; set; }

        /// <summary>
        /// Character Gender.
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }


        /// <summary>
        /// Character Faction (Alliance/Horde)
        /// </summary>
        [JsonProperty("faction")]
        public string Faction { get; set; }

        /// <summary>
        /// Amount of achievment points earned.
        /// </summary>
        [JsonProperty("achievement_points")]
        public int AchievmentPoints { get; set; }

        /// <summary>
        /// Amount of honourable kills performed.
        /// </summary>
        [JsonProperty("honorable_kills")]
        public int HonorableKills { get; set; }

        /// <summary>
        /// Gets a thumbnail for the character.
        /// </summary>
        [JsonProperty("thumbnail_url")]
        public string ThumbnailURL { get; set; }

        /// <summary>
        /// Character Region.
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }

        /// <summary>
        /// Character Realm.
        /// </summary>
        [JsonProperty("realm")]
        public string Realm { get; set; }

        /// <summary>
        /// Character Raider.io URL.
        /// </summary>
        [JsonProperty("profile_url")]
        public string RaiderIO_URL { get; set; }

        /// <summary>
        /// Pulls ILVL (Equiped and in bags) as-well as AP traits.
        /// </summary>
        [JsonProperty("gear")]
        public Gear CharGear { get; set; }

        /// <summary>
        /// Pulls current raid progression.
        /// </summary>
        [JsonProperty("raid_progression")]
        public Raid_Progression RaidProg { get; set; }

        [JsonProperty("mythic_plus_scores")]
        public Mythic_Plus_Scores CurrentMythicPlusScore { get; set; }

        [JsonProperty("previous_mythic_plus_scores")]
        public Previous_Mythic_Plus_Scores PastMythicPlusScore { get; set; }

        [JsonProperty("mythic_plus_ranks")]
        public Mythic_Plus_Ranks MythicPlusRanks { get; set; }

        [JsonProperty("mythic_plus_best_runs")]
        public Mythic_Plus_Best_Runs[] BestMythicPlus_AllTime { get; set; }

        /// <summary>
        /// Gets the top 3 weekly runs for the character.
        /// </summary>
        [JsonProperty("mythic_plus_weekly_highest_level_runs")]
        public WeeklyBestMythicPlus[] WeeklyBestMythicPlus { get; set; }

        [JsonProperty("mythic_plus_highest_level_runs")]
        public Mythic_Plus_Highest_Level_Runs[] HighestEverMythicPlus { get; set; }

        [JsonProperty("guild")]
        public Guild Guild { get; set; }
    }
}

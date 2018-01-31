using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Draxbot
{

    public class WowGuide
    {
        public Classes classes { get; set; }
    }

    public class Classes
    {
        public Hero shaman { get; set; }
        public Hero deathknight { get; set; }
        public Hero demonhunter { get; set; }
        public Hero druid { get; set; }
        public Hero hunter { get; set; }
        public Hero mage { get; set; }
        public Hero monk { get; set; }
        public Hero paladin { get; set; }
        public Hero priest { get; set; }
        public Hero rogue { get; set; }
        public Hero warlock { get; set; }
        public Hero Warrior { get; set; }
        public Hero _default { get; set; }
    }

    public class Hero
    {
        public Specs specs { get; set; }
        public string tier_token { get; set; }
        public string reply { get; set; }
    }

    public class Specs
    {
        public Data Restoration { get; set; }
        public Data Enhacement { get; set; }
        public Data Elemental { get; set; }
        public Data Frost { get; set; }
        public Data Unholy { get; set; }
        public Data Blood { get; set; }
        public Data Havoc { get; set; }
        public Data Vengence { get; set; }
        public Data Ballance { get; set; }
        public Data Feral { get; set; }
        public Data Guardian { get; set; }
        [JsonProperty("Beast Mastery")]
        public Data Beast_Mastery { get; set; }
        public Data Marksmanship { get; set; }
        public Data Survival { get; set; }
        public Data Fire { get; set; }
        public Data Arcane { get; set; }
        public Data Brewmaster { get; set; }
        public Data Mistweaver { get; set; }
        public Data Windwalker { get; set; }
        public Data Ret { get; set; }
        public Data Holy { get; set; }
        public Data Protection { get; set; }
        public Data Disc { get; set; }
        public Data Shadow { get; set; }
        public Data Subtlety { get; set; }
        public Data Assassination { get; set; }
        public Data Outlaw { get; set; }
        public Data Demonology { get; set; }
        public Data Affliction { get; set; }
        public Data Destruction { get; set; }
        public Data Arms { get; set; }
        public Data Fury { get; set; }
    }

    public class Data
    {
        public string Stats { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}

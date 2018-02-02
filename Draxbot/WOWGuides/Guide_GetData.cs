using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Draxbot
{
    public class WOWGuide_Result
    {
        public static Data Results { get; set; }
        public static string Token { get; set; }
    }
    public class WOWGuide
    {
        public static void ParseData(string _spec, string _class)
        {
            WowGuide guideInfo = JsonConvert.DeserializeObject<WowGuide>(File.ReadAllText("guides.json"));

            #region Duplicate Checking
            if (_spec.ToLower() == "holy" && _class.ToLower() == "paladin")
            {
                WOWGuide_Result.Results = guideInfo.classes.paladin.specs.Holy;
                WOWGuide_Result.Token = guideInfo.classes.paladin.tier_token;
            }
            if (_spec.ToLower() == "holy" && _class.ToLower() == "priest")
            {
                WOWGuide_Result.Results = guideInfo.classes.priest.specs.Holy;
                WOWGuide_Result.Token = guideInfo.classes.priest.tier_token;
            }
            if (_spec.ToLower() == "protection" && _class.ToLower() == "paladin")
            {
                WOWGuide_Result.Results = guideInfo.classes.paladin.specs.Protection;
                WOWGuide_Result.Token = guideInfo.classes.paladin.tier_token;
            }
            if (_spec.ToLower() == "protection" && _class.ToLower() == "warrior")
            {
                WOWGuide_Result.Results = guideInfo.classes.Warrior.specs.Protection;
                WOWGuide_Result.Token = guideInfo.classes.Warrior.tier_token;
            }
            if (_spec.ToLower() == "frost" && _class.ToLower() == "mage")
            {
                WOWGuide_Result.Results = guideInfo.classes.mage.specs.Frost;
                WOWGuide_Result.Token = guideInfo.classes.mage.tier_token;
            }
            if (_spec.ToLower() == "frost" && _class.ToLower() == "deathknight")
            {
                WOWGuide_Result.Results = guideInfo.classes.deathknight.specs.Frost;
                WOWGuide_Result.Token = guideInfo.classes.deathknight.tier_token;
            }
            if (_spec.ToLower() == "restoration" && _class.ToLower() == "druid")
            {
                WOWGuide_Result.Results = guideInfo.classes.druid.specs.Restoration;
                WOWGuide_Result.Token = guideInfo.classes.druid.tier_token;
            }
            if (_spec.ToLower() == "restoration" && _class.ToLower() == "shaman")
            {
                WOWGuide_Result.Results = guideInfo.classes.shaman.specs.Restoration;
                WOWGuide_Result.Token = guideInfo.classes.shaman.tier_token;
            }
            #endregion

            #region Switch _spec
            switch (_spec.ToLower())
            {
                case "enhancement":
                    WOWGuide_Result.Results = guideInfo.classes.shaman.specs.Enhacement;
                    WOWGuide_Result.Token = guideInfo.classes.shaman.tier_token;
                    break;
                case "elemental":
                    WOWGuide_Result.Results = guideInfo.classes.shaman.specs.Elemental;
                    WOWGuide_Result.Token = guideInfo.classes.shaman.tier_token;
                    break;
                case "unholy":
                    WOWGuide_Result.Results = guideInfo.classes.deathknight.specs.Unholy;
                    WOWGuide_Result.Token = guideInfo.classes.deathknight.tier_token;
                    break;
                case "blood":
                    WOWGuide_Result.Results = guideInfo.classes.deathknight.specs.Blood;
                    WOWGuide_Result.Token = guideInfo.classes.deathknight.tier_token;
                    break;
                case "hevoc":
                    WOWGuide_Result.Results = guideInfo.classes.demonhunter.specs.Havoc;
                    WOWGuide_Result.Token = guideInfo.classes.demonhunter.tier_token;
                    break;
                case "vengence":
                    WOWGuide_Result.Results = guideInfo.classes.demonhunter.specs.Vengence;
                    WOWGuide_Result.Token = guideInfo.classes.demonhunter.tier_token;
                    break;
                case "balance":
                    WOWGuide_Result.Results = guideInfo.classes.druid.specs.Ballance;
                    WOWGuide_Result.Token = guideInfo.classes.druid.tier_token;
                    break;
                case "feral":
                    WOWGuide_Result.Results = guideInfo.classes.druid.specs.Feral;
                    WOWGuide_Result.Token = guideInfo.classes.druid.tier_token;
                    break;
                case "guardian":
                    WOWGuide_Result.Results = guideInfo.classes.druid.specs.Guardian;
                    WOWGuide_Result.Token = guideInfo.classes.druid.tier_token;
                    break;
                case "beastmastery":
                    WOWGuide_Result.Results = guideInfo.classes.hunter.specs.Beast_Mastery;
                    WOWGuide_Result.Token = guideInfo.classes.hunter.tier_token;
                    break;
                case "marksmanship":
                    WOWGuide_Result.Results = guideInfo.classes.hunter.specs.Marksmanship;
                    WOWGuide_Result.Token = guideInfo.classes.hunter.tier_token;
                    break;
                case "survival":
                    WOWGuide_Result.Results = guideInfo.classes.hunter.specs.Survival;
                    WOWGuide_Result.Token = guideInfo.classes.hunter.tier_token;
                    break;
                case "Fire":
                    WOWGuide_Result.Results = guideInfo.classes.mage.specs.Fire;
                    WOWGuide_Result.Token = guideInfo.classes.mage.tier_token;
                    break;
                case "arcane":
                    WOWGuide_Result.Results = guideInfo.classes.mage.specs.Arcane;
                    WOWGuide_Result.Token = guideInfo.classes.mage.tier_token;
                    break;
                case "brewmaster":
                    WOWGuide_Result.Results = guideInfo.classes.monk.specs.Brewmaster;
                    WOWGuide_Result.Token = guideInfo.classes.monk.tier_token;
                    break;
                case "mistweaver":
                    WOWGuide_Result.Results = guideInfo.classes.monk.specs.Mistweaver;
                    WOWGuide_Result.Token = guideInfo.classes.monk.tier_token;
                    break;
                case "windwalker":
                    WOWGuide_Result.Results = guideInfo.classes.monk.specs.Windwalker;
                    WOWGuide_Result.Token = guideInfo.classes.monk.tier_token;
                    break;
                case "ret":
                    WOWGuide_Result.Results = guideInfo.classes.paladin.specs.Ret;
                    WOWGuide_Result.Token = guideInfo.classes.paladin.tier_token;
                    break;
                case "disc":
                    WOWGuide_Result.Results = guideInfo.classes.priest.specs.Disc;
                    WOWGuide_Result.Token = guideInfo.classes.priest.tier_token;
                    break;
                case "shadow":
                    WOWGuide_Result.Results = guideInfo.classes.priest.specs.Shadow;
                    WOWGuide_Result.Token = guideInfo.classes.priest.tier_token;
                    break;
                case "subtlety":
                    WOWGuide_Result.Results = guideInfo.classes.rogue.specs.Subtlety;
                    WOWGuide_Result.Token = guideInfo.classes.rogue.tier_token;
                    break;
                case "assassination":
                    WOWGuide_Result.Results = guideInfo.classes.rogue.specs.Assassination;
                    WOWGuide_Result.Token = guideInfo.classes.rogue.tier_token;
                    break;
                case "outlaw":
                    WOWGuide_Result.Results = guideInfo.classes.rogue.specs.Outlaw;
                    WOWGuide_Result.Token = guideInfo.classes.rogue.tier_token;
                    break;
                case "demonology":
                    WOWGuide_Result.Results = guideInfo.classes.warlock.specs.Demonology;
                    WOWGuide_Result.Token = guideInfo.classes.warlock.tier_token;
                    break;
                case "affliction":
                    WOWGuide_Result.Results = guideInfo.classes.warlock.specs.Affliction;
                    WOWGuide_Result.Token = guideInfo.classes.warlock.tier_token;
                    break;
                case "destruction":
                    WOWGuide_Result.Results = guideInfo.classes.warlock.specs.Destruction;
                    WOWGuide_Result.Token = guideInfo.classes.warlock.tier_token;
                    break;
                case "arms":
                    WOWGuide_Result.Results = guideInfo.classes.Warrior.specs.Arms;
                    WOWGuide_Result.Token = guideInfo.classes.Warrior.tier_token;
                    break;
                case "fury":
                    WOWGuide_Result.Results = guideInfo.classes.Warrior.specs.Fury;
                    WOWGuide_Result.Token = guideInfo.classes.Warrior.tier_token;
                    break;
                default:
                    break;
            }
            #endregion
        }
    }

}

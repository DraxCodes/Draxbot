using System;
using System.Collections.Generic;
using System.Text;

namespace Draxbot
{
    public class WOWGuide_Spec
    {
        public static string Check(string s)
        {
            string result = null;
            switch (s)
            {
                case "enhance":
                    result = "enhancement";
                    break;
                case "prot":
                    result = "protection";
                    break;
                case "boomy":
                    result = "balance";
                    break;
                case "kitty":
                    result = "feral";
                    break;
                case "destro":
                    result = "destruction";
                    break;
                case "aff":
                    result = "affliction";
                    break;
                case "demo":
                    result = "demonology";
                    break;
                case "sub":
                    result = "subtlety";
                    break;
                case "ass":
                    result = "assassination";
                    break;
                case "bm":
                    result = "beastmastery";
                    break;
                case "mm":
                    result = "marksmanship";
                    break;
                case "bear":
                    result = "guardian";
                    break;
                case "resto":
                    result = "restoration";
                    break;
                case "ele":
                    result = "elemental";
                    break;
                default:
                    result = s;
                    break;
            }

            return result;
        }
    }
}

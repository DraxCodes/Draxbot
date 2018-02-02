using System;
using System.Collections.Generic;
using System.Text;

namespace Draxbot
{
    public class WOWGuide_Class
    {
        public static string Check(string s)
        {
            string result = null;
            switch (s)
            {
                case "dk":
                    result = "deathknight";
                    break;
                case "dh":
                    result = "demonhunter";
                    break;
                case "pala":
                    result = "paladin";
                    break;
                default:
                    result = s;
                    break;
            }
            return result;
        }
    }
}

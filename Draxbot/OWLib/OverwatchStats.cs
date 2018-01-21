using System;
using System.Collections.Generic;
using System.Text;

namespace Draxbot
{
    public class OWStats
    {
        public string username { get; set; }
        public int level { get; set; }
        public string portrait { get; set; }
        public Games games { get; set; }
        public Playtime playtime { get; set; }
        public Competitive1 competitive { get; set; }
        public string levelFrame { get; set; }
        public string star { get; set; }
    }

    public class Games
    {
        public Quickplay quickplay { get; set; }
        public Competitive competitive { get; set; }
    }

    public class Quickplay
    {
        public int won { get; set; }
    }

    public class Competitive
    {
        public int won { get; set; }
        public int lost { get; set; }
        public int draw { get; set; }
        public int played { get; set; }
    }

    public class Playtime
    {
        public string quickplay { get; set; }
        public string competitive { get; set; }
    }

    public class Competitive1
    {
        public int rank { get; set; }
        public string rank_img { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RaiderIO_Lib
{
    public class Mythic_Plus_Ranks
    {
        public Overall overall { get; set; }
        public Dps dps { get; set; }
        public Healer healer { get; set; }
        public Tank tank { get; set; }
        public Class1 _class { get; set; }
        public Class_Dps class_dps { get; set; }
        public Class_Healer class_healer { get; set; }
        public Class_Tank class_tank { get; set; }
    }

    public class Overall
    {
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class Dps
    {
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class Healer
    {
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class Tank
    {
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class Class1
    {
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class Class_Dps
    {
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class Class_Healer
    {
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class Class_Tank
    {
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

}

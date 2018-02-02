using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RaiderIO_Library
{
    public class Gear : Character
    {
        [JsonProperty("item_level_equipped")]
        public int EquipedILVL { get; set; }
        [JsonProperty("item_level_total")]
        public int TotalILVL { get; set; }
        [JsonProperty("artifact_traits")]
        public int APTraits { get; set; }
    }
}


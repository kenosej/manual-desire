using System.Collections.Generic;

namespace AutoInfo
{
    public class Car
    {
        public string Name { get; set; }
        public int NumberOfGears { get; set; }
        public int MaxTorque { get; set; }
        public int MinTorque { get; set; }
        public int Weight { get; set; }
        public List<Gear> Gears { get; set; }
    }
}
using System.Linq;
using System.Collections.Generic;

namespace Models
{
    public class Car
    {
        public string Name { get; set; }
        public int NumberOfGears { get; set; }
        public int Weight { get; set; }
        public List<Gear> Gears { get; set; }
        // calculated
        public Gear GearReverse // just a reference to the 1st gear, cuz they are often the same
        {
            get
            {
                return Gears.Find(g => g.Level == 1); // chance for a bug
            }
        }

        public float MinScaledRadianEndpoint
        {
            get
            {
                return Gears.Min(g => g.ScaledRadianEndpoint); // chance for a bug
            }
        }
    }
}
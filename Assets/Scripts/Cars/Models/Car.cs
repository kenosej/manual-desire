using System.Linq;
using Cars.Movement;
using System.Collections.Generic;

namespace Cars.Models
{
    public class Car
    {
        public string Name { get; set; }
        public string PrefabName { get; set; }
        public int Weight { get; set; }
        public string Drive { get; set; }
        public string SoundFilename { get; set; }
        public List<Gear> Gears { get; set; }
        
        // calculated
        public int NumberOfGears => Gears.Count;
        
        public Gear GearReverse => Gears.Find(g => g.Level == 1); // just a reference to the 1st gear, cuz they are often the same

        public float MinScaledRadianEndpoint => Gears.Min(g => g.ScaledRadianEndpoint); // chance for a bug

        public ParentControl.Drive CalcDrive
        {
            get
            {
                switch (Drive)
                {
                    case "FRONT":
                        return ParentControl.Drive.Front;
                    case "REAR":
                        return ParentControl.Drive.Rear;
                    default:
                        return ParentControl.Drive.All;
                }
            }
        }

        public float TotalMaxSpeed => Gears.Max(g => g.MaxSpeedKmh);
    }
}
using UnityEngine;

namespace AutoInfo
{
    public class Gear
    {
        public int Level { get; set; }
        public float MaxSpeed { get; set; }
        public float MaxSpeedKmh { get; set; }
        public float LowestTorque { get; set; }
        public float HighestTorque { get; set; }
        public float Numerator { get; set; }
        public float Denominator { get; set; }
        
        // calculated
        public float ScaledRadianEndpoint 
        {
            get
            {
                return Mathf.PI * (Denominator / Numerator);
            } 
        }
    }
}
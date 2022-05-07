using UnityEngine;

namespace Models
{
    public class Gear
    {
        public int Level { get; set; }
        public float MinSpeedKmh { get; set; }
        public float MaxSpeedKmh { get; set; }
        public float LowestTorque { get; set; }
        public float HighestTorque { get; set; }
        public float Numerator { get; set; }
        public float Denominator { get; set; }
        
        // calculated
        public float RadianScalar => Numerator / Denominator;
        public float ScaledRadianEndpoint => Mathf.PI * (Denominator / Numerator);

        public float ScaledRadianPeak => ScaledRadianEndpoint / 2f;

        public float DeltaTorque => HighestTorque - LowestTorque;
    }
}
using UnityEngine;

namespace Movement
{
    public class ParentControl : MonoBehaviour
    {
        public GameObject[] _wheelsMesh;
        public WheelCollider[] _wheelsColliders;

        public bool _throttle;
        public bool _brake;
        public bool _left;
        public bool _right;

        public enum Gears
        {
            FIRST = 1,
            SECOND,
            THIRD,
            FOURTH,
            FIFTH,
            SIXTH,
            SEVENTH,
            EIGHTH,
            REVERSE,
            NEUTRAL
        };

        [field: SerializeField] public Gears CurrentGear { get; set; } = Gears.NEUTRAL;
        [field: SerializeField] public bool Clutch { get; set; }
        [field: SerializeField] public bool ShiftingReady { get; set; }
        
        // without additional scaling, max value is PI, which corresponds to positive half of sin wave
        [field: SerializeField] public float DeltaRadianScalar;

        [field: SerializeField] public bool Gear1 { get; set; }
        [field: SerializeField] public bool Gear2 { get; set; }
        [field: SerializeField] public bool Gear3 { get; set; }
        [field: SerializeField] public bool Gear4 { get; set; }
        [field: SerializeField] public bool Gear5 { get; set; }
        [field: SerializeField] public bool Gear6 { get; set; }
        [field: SerializeField] public bool Gear7 { get; set; }
        [field: SerializeField] public bool Gear8 { get; set; }
        [field: SerializeField] public bool GearReverse { get; set; }
        [field: SerializeField] public bool GearNeutral { get; set; }
    }
}
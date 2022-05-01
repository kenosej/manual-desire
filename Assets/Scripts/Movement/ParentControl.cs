using AutoInfo;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Movement
{
    public class ParentControl : MonoBehaviour
    {
        public enum Gears { NEUTRAL, FIRST, SECOND, THIRD, FOURTH, FIFTH, SIXTH, SEVENTH, EIGHTH, REVERSE };
        
        public GameObject[] _wheelsMesh;
        public WheelCollider[] _wheelsColliders;
        public Car Car { get; set; }

        public bool _brake;
        public bool _left;
        public bool _right;
        public bool _throttle;
        
        [field: SerializeField] public bool Clutch { get; set; }
        [field: SerializeField] public bool ShiftingReady { get; set; }
        
        [field: SerializeField] public Gears CurrentGear { get; set; } = Gears.NEUTRAL;
        [field: SerializeField] public bool[] GearsReceiver { get; set; } = new bool[10]; // NEUTRAL, 1-8, REVERSE
        
        // without additional scaling, max value is PI, which corresponds to the positive half of sin wave
        [field: SerializeField] public float Radian;

        private void Awake()
        {
            FetchCarInfoFromJson();
        }
        private void FetchCarInfoFromJson()
        {
            var fs = new FileStream("./Assets/AutoInfo/PurpleFirst.json", FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs);
            
            Car = JsonConvert.DeserializeObject<Car>(sr.ReadToEnd());
            
            LogDeltaMaxScalarPerGears();
        }

        private void LogDeltaMaxScalarPerGears()
        {
            foreach (Gear gear in Car.Gears)
            {
                 Debug.Log($"Gear {gear.Level}, Max DeltaRadianScalar: {Mathf.PI * (gear.Denominator / gear.Numerator)}");
            }
        }
    }
}

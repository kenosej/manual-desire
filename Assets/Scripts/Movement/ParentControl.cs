using AutoInfo;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Serialization;

namespace Movement
{
    public class ParentControl : MonoBehaviour
    {
        public enum GearsEnum { NEUTRAL, FIRST, SECOND, THIRD, FOURTH, FIFTH, SIXTH, SEVENTH, EIGHTH, REVERSE };
        
        public GameObject[] _wheelsMesh;
        public WheelCollider[] _wheelsColliders;
        public Car Car { get; set; }

        public bool _brake;
        public bool _left;
        public bool _right;
        public bool _throttle;
        
        [field: SerializeField] public bool Clutch { get; set; }
        [field: SerializeField] public bool ShiftingReady { get; set; }
        
        [field: SerializeField] public GearsEnum CurrentGear { get; set; } = GearsEnum.NEUTRAL;
        [field: SerializeField] public bool[] GearsReceiver { get; set; } = new bool[10]; // NEUTRAL, 1-8, REVERSE
        
        // without additional scaling, max value is PI, which corresponds to the positive half of sin wave
        [SerializeField] private float _radian;

        public float Radian
        {
            get => _radian;
            set
            {
                if (ShouldSmoothAlignRadian)
                {
                    float scaledRadianEndpoint = FindCorrectRadianEndpointToGear();
                    
                    if (ShouldSmoothAlignRadianUpOrDown)
                    {
                        if (SmoothAligningRadian < value)
                        {
                            SmoothAligningRadian += scaledRadianEndpoint * 0.01f;
                        }
                        else
                        {
                            ShouldSmoothAlignRadian = false;
                        }
                    }
                    else
                    {
                        if (SmoothAligningRadian > value)
                        {
                            SmoothAligningRadian -= scaledRadianEndpoint * 0.01f;
                        }
                        else
                        {
                            ShouldSmoothAlignRadian = false;
                        }
                    }
                }

                if (value <= 0f) return;

                _radian = value;
            }
        }
        
        [SerializeField] public bool ShouldSmoothAlignRadian;
        [SerializeField] public bool ShouldSmoothAlignRadianUpOrDown; // up (true) when switching to lower gear

        [SerializeField] private float _smoothAligningRadian;

        public float SmoothAligningRadian
        {
            get => _smoothAligningRadian;
            private set => _smoothAligningRadian = value;
        }

        public void SetSmoothAligningRadianIntoNewGearScale(in float radian, in float nextGearScaledRadianEndpoint)
        {
            _smoothAligningRadian = nextGearScaledRadianEndpoint * radian / FindCorrectRadianEndpointToGear();
        }
        

        public float FindCorrectRadianEndpointToGear()
        {
            float scaledRadianEndpoint;
            
            if (CurrentGear == GearsEnum.NEUTRAL)
            {
                scaledRadianEndpoint = Car.MinScaledRadianEndpoint;
            }
            else if (CurrentGear == GearsEnum.REVERSE)
            {
                scaledRadianEndpoint = Car.GearReverse.ScaledRadianEndpoint;
            }
            else
            {
                scaledRadianEndpoint = Car.Gears.Find(g => g.Level == (int)CurrentGear).ScaledRadianEndpoint;
            }

            return scaledRadianEndpoint;
        }

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

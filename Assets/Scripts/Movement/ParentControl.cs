using Models;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Movement
{
    public class ParentControl : MonoBehaviour
    {
        public enum Drive { FRONT, REAR, ALL };
        public enum GearsEnum { NEUTRAL, FIRST, SECOND, THIRD, FOURTH, FIFTH, SIXTH, SEVENTH, EIGHTH, REVERSE };
        
        public GameObject[] _wheelsMesh;
        public WheelCollider[] _wheelsColliders;
        public Car Car { get; set; }
        public bool IsCarDead { get; set; }
        [field: SerializeField] public bool IsTurnedOn { get; set; }

        [SerializeField] private float _heat;
        
        public float Heat
        {
            get => _heat;
            set
            {
                if (value < 0f || value > 100f) return;
                _heat = value;
            }
        }

        [SerializeField] private float _damage;
        
        public float Damage
        {
            get => _damage;
            set
            {
                if (value > 100 || value < 0) return;
                _damage = value;
            }
        }

        public bool _parkingBrake;
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
                ManageSmoothAlignRadian(in value);

                if (value < 0f || value > FindCorrectRadianEndpointToGear()) return;

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
        
        private void ManageSmoothAlignRadian(in float value)
        {
            if (!ShouldSmoothAlignRadian) return;
            
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

        private void Awake()
        {
            FetchCarInfoFromJson();
        }
        
        private void FetchCarInfoFromJson()
        {
            var fs = new FileStream("./Assets/CarsInfo/PurpleFirst.json", FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs);
            
            Car = JsonConvert.DeserializeObject<Car>(sr.ReadToEnd());
            
            LogDeltaMaxScalarPerGears();
        }

        private void LogDeltaMaxScalarPerGears()
        {
            foreach (Gear gear in Car.Gears)
            {
                 Debug.Log($"Gear {gear.Level}, ScaledRadianEndpoint: {gear.ScaledRadianEndpoint}");
            }
        }
        
        public void ApplyTorqueToWheels(in float torque)
        {
            for (var i = 0; i < _wheelsColliders.Length; i++)
            {
                switch (Car.CalcDrive)
                {
                    case Drive.FRONT:
                    {
                        if (i < 2)
                            _wheelsColliders[i].motorTorque = torque;

                        break;
                    }
                    case Drive.REAR:
                    {
                        if (i > 1)
                            _wheelsColliders[i].motorTorque = torque;

                        break;
                    }
                    default:
                        _wheelsColliders[i].motorTorque = torque;
                        break;
                }
            }
        }

        private void Update()
        {
            CoordinateWheelMeshes(); 
        }

        private void CoordinateWheelMeshes()
        {
            CoordinateWheelMeshesSideways();
            CoordinateWheelMeshesForwards();
        }
        
        private void CoordinateWheelMeshesSideways()
        {
            _wheelsMesh[0].transform.localEulerAngles = new Vector3(
                    _wheelsMesh[0].transform.localEulerAngles.x,
                    _wheelsColliders[0].steerAngle - _wheelsMesh[0].transform.localEulerAngles.z,
                    _wheelsMesh[0].transform.localEulerAngles.z
                );
            
            _wheelsMesh[1].transform.localEulerAngles = new Vector3(
                    _wheelsMesh[1].transform.localEulerAngles.x,
                    _wheelsColliders[1].steerAngle - _wheelsMesh[1].transform.localEulerAngles.z,
                    _wheelsMesh[1].transform.localEulerAngles.z
                );
        }

        private void CoordinateWheelMeshesForwards()
        {
            for (int i = 0; i < _wheelsMesh.Length; i++)
            {
                _wheelsMesh[i].transform.Rotate(_wheelsColliders[i].rpm / 60 * 360, 0, 0);
            }
        }
    }
}

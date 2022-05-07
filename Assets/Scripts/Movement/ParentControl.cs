using Models;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Movement
{
    public class ParentControl : MonoBehaviour
    {
        public enum Drive { Front, Rear, All };
        public enum GearsEnum { Neutral, First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Reverse };
        
        public GameObject[] wheelsMesh; 
        public WheelCollider[] wheelsColliders;
        public Car Car { get; private set; }
        public bool IsCarDead { get; set; }
        [field: SerializeField] public bool IsTurnedOn { get; set; }

        [SerializeField] private float heat;
        
        public float Heat
        {
            get => heat;
            set
            {
                if (value < 0f || value > 100f) return;
                heat = value;
            }
        }

        [SerializeField] private float damage;
        
        public float Damage
        {
            get => damage;
            set
            {
                if (value > 100 || value < 0) return;
                damage = value;
            }
        }

        public bool parkingBrake;
        public bool brake;
        public bool left;
        public bool right;
        public bool throttle;
        
        [field: SerializeField] public bool Clutch { get; set; }
        [field: SerializeField] public bool ShiftingReady { get; set; }
        
        [field: SerializeField] public GearsEnum CurrentGear { get; set; } = GearsEnum.Neutral;
        [field: SerializeField] public bool[] GearsReceiver { get; set; } = new bool[10]; // NEUTRAL, 1-8, REVERSE
        
        // without additional scaling, max value is PI, which corresponds to the positive half of sin wave
        [SerializeField] private float radian;

        public float Radian
        {
            get => radian;
            set
            {
                ManageSmoothAlignRadian(in value);

                if (value < 0f || value > FindCorrectRadianEndpointToGear()) return;

                radian = value;
            }
        }

        [SerializeField] public bool shouldSmoothAlignRadian;
        [SerializeField] public bool shouldSmoothAlignRadianUpOrDown; // up (true) when switching to lower gear
        
        [SerializeField] private float smoothAligningRadian;

        public float SmoothAligningRadian
        {
            get => smoothAligningRadian;
            private set => smoothAligningRadian = value;
        }

        public void SetSmoothAligningRadianIntoNewGearScale(in float parameterRadian, in float nextGearScaledRadianEndpoint)
        {
            smoothAligningRadian = nextGearScaledRadianEndpoint * parameterRadian / FindCorrectRadianEndpointToGear();
        }

        public float FindCorrectRadianEndpointToGear()
        {
            float scaledRadianEndpoint;
            
            if (CurrentGear == GearsEnum.Neutral)
            {
                scaledRadianEndpoint = Car.MinScaledRadianEndpoint;
            }
            else if (CurrentGear == GearsEnum.Reverse)
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
            if (!shouldSmoothAlignRadian) return;
            
            var scaledRadianEndpoint = FindCorrectRadianEndpointToGear();
            
            if (shouldSmoothAlignRadianUpOrDown)
            {
                if (SmoothAligningRadian < value)
                    SmoothAligningRadian += scaledRadianEndpoint * 0.01f;
                else
                    shouldSmoothAlignRadian = false;
            }
            else
            {
                if (SmoothAligningRadian > value)
                    SmoothAligningRadian -= scaledRadianEndpoint * 0.01f;
                else
                    shouldSmoothAlignRadian = false;
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
                 Debug.Log($"Gear {gear.Level}, ScaledRadianEndpoint: {gear.ScaledRadianEndpoint}");
        }
        
        public void ApplyTorqueToWheels(in float torque)
        {
            for (var i = 0; i < wheelsColliders.Length; i++)
            {
                switch (Car.CalcDrive)
                {
                    case Drive.Front:
                    {
                        if (i < 2)
                            wheelsColliders[i].motorTorque = torque;

                        break;
                    }
                    case Drive.Rear:
                    {
                        if (i > 1)
                            wheelsColliders[i].motorTorque = torque;

                        break;
                    }
                    default:
                        wheelsColliders[i].motorTorque = torque;
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
            wheelsMesh[0].transform.localEulerAngles = new Vector3(
                    wheelsMesh[0].transform.localEulerAngles.x,
                    wheelsColliders[0].steerAngle - wheelsMesh[0].transform.localEulerAngles.z,
                    wheelsMesh[0].transform.localEulerAngles.z
                );
            
            wheelsMesh[1].transform.localEulerAngles = new Vector3(
                    wheelsMesh[1].transform.localEulerAngles.x,
                    wheelsColliders[1].steerAngle - wheelsMesh[1].transform.localEulerAngles.z,
                    wheelsMesh[1].transform.localEulerAngles.z
                );
        }

        private void CoordinateWheelMeshesForwards()
        {
            for (int i = 0; i < wheelsMesh.Length; i++)
                wheelsMesh[i].transform.Rotate(wheelsColliders[i].rpm / 60 * 360, 0, 0);
        }
    }
}

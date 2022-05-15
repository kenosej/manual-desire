using System.IO;
using System.Linq;
using Cars.Models;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cars.Movement
{
    //[RequireComponent(typeof(Rigidbody))]
    public class ParentControl : MonoBehaviour
    {
        public enum Drive { Front, Rear, All };
        public enum GearsEnum { Neutral, First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Reverse };
        
        public bool IsPaused { get; set; }

        public Vector3 CenterOfMass = new Vector3(0f, 0.66f, 0f);
        
        private Rigidbody _rB;
        
        public GameObject[] wheelsMesh; 
        public WheelCollider[] wheelsColliders;
        public Car Car { get; set; }
        public bool IsCarDead { get; set; }
        [field: SerializeField] public bool IsTurnedOn { get; set; }
        
        private float _healthBarFillAmount = 1f;

        public float HealthBarFillAmount
        {
            get => _healthBarFillAmount;
            set
            {
                switch (value)
                {
                    case <= 0f:
                        IsCarDead = true;
                        return;
                    case > 1f:
                        return;
                    default:
                        _healthBarFillAmount = value;
                        return;
                }
            }
        }

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

        public bool parkingBrake;
        public bool brake;
        public bool left;
        public bool right;
        public bool throttle;

        public float SpeedInKmh => _rB.velocity.magnitude * 3.6f;

        public float SpeedInKmhFromRPM
        {
            get
            {
                var avgWheelRadius = wheelsColliders.Average(wc => wc.radius);
                
                var perimeter = avgWheelRadius * Mathf.PI * 2f;

                float avgRPMOfDriveWheels;

                if (Car.CalcDrive == Drive.Front)
                {
                    avgRPMOfDriveWheels = wheelsColliders.Take(2).Average(wc => wc.rpm);
                }
                else if (Car.CalcDrive == Drive.Rear)
                {
                    avgRPMOfDriveWheels = wheelsColliders.TakeLast(2).Average(wc => wc.rpm);
                }
                else
                {
                    avgRPMOfDriveWheels = wheelsColliders.Average(wc => wc.rpm);
                }
                
                // * 60, cuz from m/min to m/h, then /1000 to km/h
                return Mathf.Abs(perimeter * avgRPMOfDriveWheels * 60 / 1000);
            }
        }
        
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
                if (value < 0f || value > FindCorrectRadianEndpointToGear()) return;

                radian = value;
            }
        }

        public float FindCorrectRadianEndpointToGear()
        {
            float scaledRadianEndpoint;
            
            switch (CurrentGear)
            {
                case GearsEnum.Neutral:
                    scaledRadianEndpoint = Car.MinScaledRadianEndpoint;
                    break;
                case GearsEnum.Reverse:
                    scaledRadianEndpoint = Car.GearReverse.ScaledRadianEndpoint;
                    break;
                default:
                    scaledRadianEndpoint = Car.Gears.Find(g => g.Level == (int)CurrentGear).ScaledRadianEndpoint;
                    break;
            }

            return scaledRadianEndpoint;
        }

        public float FindCorrectMinSpeedToGear()
        {
            float minSpeed;
            
            switch (CurrentGear)
            {
                case GearsEnum.Neutral:
                    minSpeed = 0f;
                    break;
                case GearsEnum.Reverse:
                    minSpeed = Car.GearReverse.MinSpeedKmh;
                    break;
                default:
                    minSpeed = Car.Gears.Find(g => g.Level == (int)CurrentGear).MinSpeedKmh;
                    break;
            }

            return minSpeed;
        }

        public float FindCorrectMaxSpeedToGear()
        {
            float maxSpeed;
            
            switch (CurrentGear)
            {
                case GearsEnum.Neutral:
                    maxSpeed = float.MinValue; // to avoid division with 0
                    break;
                case GearsEnum.Reverse:
                    maxSpeed = Car.GearReverse.MaxSpeedKmh;
                    break;
                default:
                    maxSpeed = Car.Gears.Find(g => g.Level == (int)CurrentGear).MaxSpeedKmh;
                    break;
            }

            return maxSpeed;
        }
        
        private float _RPMNeedle01Position;

        public float RPMNeedle01Position
        {
            get => _RPMNeedle01Position;
            set
            {
                if (value < 0f || value > 1f) return;
                _RPMNeedle01Position = value;
            }
        }

        private void Awake()
        {
            _rB = GetComponent<Rigidbody>();
            Debug.Log($"Auto-calculated center of mass: {_rB.centerOfMass.y} (y)");
            _rB.centerOfMass = CenterOfMass;
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

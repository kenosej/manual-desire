using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OnScreen
{
    public class MovingSpeedNeedle : MonoBehaviour
    {
        private Image _needle;
        [SerializeField] private Rigidbody _rB;
        public float SpeedInKmh
        {
            get => _rB.velocity.magnitude * 3.6f;
        }

        private Vector3 _currRotation;

        private readonly float[] _positions =
        {
            -12.359f,
            -34.819f,
            -58.186f,
            -82.823f,
            -108.744f,
            -134.637f,
            -160.955f,
            -185.327f,
            -209.259f,
            -232.145f,
            -254.883f
        };

        private readonly float[] _kmhPositions = { 0, 20, 40, 60, 80, 100, 120, 140, 160, 180, 200 };
        [field: SerializeField] public int ActiveSpeedRegion { get; set; }

        private void Awake()
        {
            _needle = GetComponent<Image>();
            
            _currRotation = transform.eulerAngles;

            // useful for setting up needle positions
            
            //_currRotation.z = _currRotation.z % 360;
            //if (_currRotation.z > 180)
            //    _currRotation.z -= 360f;
            //
            //Debug.Log(_currRotation.z);
            //UpdateNeedlePosition(_currRotation.z);
            //Debug.Log(_currRotation.z);
            //UpdateNeedlePosition(_positions[1]);
        }

        private void UpdateNeedlePosition(in float rot)
        {
            _currRotation.z = rot;
            transform.rotation = Quaternion.Euler(_currRotation);
        }

        private void Update()
        {
            DecideSpeedRegion();
            UpdateNeedlePosition(ScaleNeedlePositionToSpeed());
        }

        private float ScaleNeedlePositionToSpeed()
        {
            float minInput = _kmhPositions[ActiveSpeedRegion];
            float maxInput = _kmhPositions[ActiveSpeedRegion + 1];
            float minOutput = _positions[ActiveSpeedRegion];
            float maxOutput = _positions[ActiveSpeedRegion + 1];

            float numerator = (maxOutput - minOutput) * (SpeedInKmh - minInput);
            float denominator = maxInput - minInput;

            return numerator / denominator + minOutput;
        }

        private void DecideSpeedRegion()
        {
            for (int i = 0; i < _kmhPositions.Length - 1; i++)
            {
                if (SpeedInKmh >= _kmhPositions[i] && SpeedInKmh < _kmhPositions[i + 1])
                {
                    ActiveSpeedRegion = i;
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
public class Steering : MonoBehaviour
{
    private ParentControl _pC;
    const float LEFT_MAX_ANGLE = -30f;
    const float RIGHT_MAX_ANGLE = 30f;
    [field: SerializeField] float CurrSteerAngle { get; set; }
    [field: SerializeField] float SteerMult { get; set; } = 1.5f;
   
    private void Awake()
    {
        _pC = GetComponent<ParentControl>();
    }

    private void FixedUpdate()
    {
        Steer();
        ReturnSteeringWheelToCenter();
    }

    private void Steer()
    {
        if (_pC._left) TurnLeft();
        if (_pC._right) TurnRight();
    }

    private void TurnLeft()
    {
        if (CurrSteerAngle < 0.1f && CurrSteerAngle > -0.1f) CurrSteerAngle = -0.1f;

        bool isTurnedRight = CurrSteerAngle > 0;
        float newAngle;

        if (isTurnedRight)
        {
            newAngle = CurrSteerAngle / SteerMult - 0.1f;
        }
        else
        {
            newAngle = CurrSteerAngle * SteerMult;
        }
            
        if (newAngle > LEFT_MAX_ANGLE)
        {
            CurrSteerAngle = newAngle;
        }
        
        for (var i = 0; i < _pC._wheelsColliders.Length; i++)
        {
            if (i < 2)
            {
                _pC._wheelsColliders[i].steerAngle = CurrSteerAngle;
            }
        }
    }
    
    private void TurnRight()
    {
        if (CurrSteerAngle > -0.1f && CurrSteerAngle < 0.1f) CurrSteerAngle = 0.1f;
        
        bool isTurnedLeft = CurrSteerAngle < 0;
        float newAngle;
        
        if (isTurnedLeft)
        {
            newAngle = CurrSteerAngle / SteerMult + 0.1f;
        }
        else
        {
            newAngle = CurrSteerAngle * SteerMult;
        }
        
        if (newAngle < RIGHT_MAX_ANGLE)
        {
            CurrSteerAngle = newAngle;
        }
        
        for (var i = 0; i < _pC._wheelsColliders.Length; i++)
        {
            if (i < 2)
            {
                _pC._wheelsColliders[i].steerAngle = CurrSteerAngle;
            }
        }
    }
    
    private void ReturnSteeringWheelToCenter()
    {
        if (!_pC._left && !_pC._right && CurrSteerAngle != 0f)
        {
            CurrSteerAngle /= SteerMult;

            if (Math.Abs(CurrSteerAngle) < 0.1)
            {
                CurrSteerAngle = 0;
            }
            
            for (var i = 0; i < _pC._wheelsColliders.Length; i++)
            {
                if (i < 2)
                {
                    _pC._wheelsColliders[i].steerAngle = CurrSteerAngle;
                }
            }
        }
    }
}
}

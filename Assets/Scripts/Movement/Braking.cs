using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
public class Braking : MonoBehaviour
{
    private ParentControl _pC;
    [field: SerializeField] float BrakeTorque { get; set; } = 7000f;
    
    private void Awake()
    {
        _pC = GetComponent<ParentControl>();
    }
    
    private void FixedUpdate()
    {
        if (_pC._brake)
        {
            foreach (var wheelCollider in _pC._wheelsColliders)
            {
                wheelCollider.brakeTorque = BrakeTorque;
            }

            //Debug.Log(_wheelsColliders[0].brakeTorque);
        }

        if (!_pC._brake)
        {
            foreach (var wheelCollider in _pC._wheelsColliders)
            {
                wheelCollider.brakeTorque = 0f;
            }
        }
    }
    
}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController_Test : MonoBehaviour
{
    public WheelCollider[] wheelCol;
    public float acceleration;
    public float breakingForce;

    private float _currentArc;
    private float _currentBreakingFroce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentArc = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _currentBreakingFroce = breakingForce;
        }
        else 
        {
            _currentBreakingFroce = 0;
        }

        wheelCol[1].motorTorque = _currentArc;
        foreach (var wheelCol in wheelCol) 
        {
            wheelCol.brakeTorque = _currentBreakingFroce;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 1500f;
    public float minimumSpeed = 10f;
    public float maxSteer = 20f;
    public float brakeForce = 300f;
    public float engineBrakeForce = 100f;

    public float Steer { get; set; }
    public float Throttle { get; set; }
    public bool ShouldBrake { get; set; }

    protected float currentSpeed;

    private Rigidbody _rigidbody;
    private Wheel[] wheels;
    
    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void Update()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;

        foreach(var wheel in wheels)
        {
            if(ShouldBrake)
            {
                wheel.BrakeTorque = brakeForce;
            }
            else 
            {
                if (Throttle == 0 && currentSpeed > minimumSpeed)
                {
                    wheel.BrakeTorque = engineBrakeForce;
                }
                else
                {
                    wheel.Torque = Throttle * motorTorque;
                    wheel.BrakeTorque = 0;
                }
            }
            wheel.SteerAngle = Steer * maxSteer;
        }
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }
}

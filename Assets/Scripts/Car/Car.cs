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

    private float steer;
    private float throttle;

    public Vector3 Position 
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public float Steer 
    { 
        get { return steer; }
        set { steer = Mathf.Min(1, Mathf.Max(-1, value)); } 
    }

    public float Throttle 
    { 
        get { return throttle; }
        set { throttle = Mathf.Min(1, Mathf.Max(-1, value)); } 
    }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Character
{
    public float motorTorque = 1500f;
    public float minimumSpeed = 10f;
    public float maxSteer = 20f;
    public float brakeForce = 300f;
    public float engineBrakeForce = 100f;

    public AudioSource source;

    private float steer;
    public float Steer 
    { 
        get { return steer; }
        set { steer = Mathf.Min(1, Mathf.Max(-1, value)); } 
    }

    private float throttle;
    public float Throttle 
    { 
        get { return throttle; }
        set { throttle = Mathf.Min(1, Mathf.Max(-1, value)); } 
    }

    public bool ShouldBrake { get; set; }

    private Wheel[] wheels;
    
    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        source = GetComponent<AudioSource>();
    }

    protected override void Run()
    {
        Speed = rigidbody.velocity.magnitude;
        UpdatePitch("CarEngine", Speed);

        foreach(var wheel in wheels)
        {
            if(ShouldBrake)
            {
                wheel.BrakeTorque = brakeForce;
            }
            else 
            {
                if (Throttle == 0 && Speed > minimumSpeed)
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

    public void UpdatePitch (string name, float speed)
    {
        if (source == null) return;
        source.pitch =  Mathf.InverseLerp(0f, 30f, speed) + 1.3f;
    }
}

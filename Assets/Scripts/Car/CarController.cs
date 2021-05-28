using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class CarController : MonoBehaviour
{
    public float TimeStillNeededToReset = 3.0f;
    public float PositionChangeToResetThreshold = 0.01F;

    protected Car car;

    private Vector3 lastPosition;
    private float timeSinceTransformChange = 0;

    private void Start()
    {
        car = GetComponent<Car>();
        lastPosition = car.transform.position;
    }

    public virtual void Update()
    {
        if (timeSinceTransformChange >= TimeStillNeededToReset)
        {
            ResetCar();
        }

        if ((lastPosition - car.transform.position).magnitude < PositionChangeToResetThreshold)
        {
            timeSinceTransformChange += Time.deltaTime;
        }
        else
        {
            timeSinceTransformChange = 0;
        }

        lastPosition = car.transform.position;
    }

    public void ResetCar()
    {
        Transform checkpointTransform = car.GetComponent<Laps>().LastCheckpointTransform();
        car.transform.position = checkpointTransform.position + new Vector3(0,-1.1F,0);
        car.transform.eulerAngles = checkpointTransform.eulerAngles;
        car.transform.Rotate(0, 180, 0); 

        Rigidbody carRigidbody = car.GetComponent<Rigidbody>();
        carRigidbody.velocity = new Vector3(0,0,0);
    }
}

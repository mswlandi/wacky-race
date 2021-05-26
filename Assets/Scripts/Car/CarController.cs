using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarController : MonoBehaviour
{
    protected Car car;

    private void Start()
    {
        car = GetComponent<Car>();
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCar();
        }
    }

    public void ResetCar()
    {
        Transform checkpointTransform = car.GetComponent<Laps>().LastCheckpointTransform();
        car.transform.position = checkpointTransform.position + new Vector3(0,-1.1F,0);
        car.transform.eulerAngles = checkpointTransform.eulerAngles;

        Rigidbody carRigidbody = car.GetComponent<Rigidbody>();
        carRigidbody.velocity = new Vector3(0,0,0);
    }
}

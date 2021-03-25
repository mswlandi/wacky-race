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
    public abstract void Update();
}

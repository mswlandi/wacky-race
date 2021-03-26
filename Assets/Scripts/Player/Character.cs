using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Transform centerOfMass;
    protected Rigidbody rigidbody;
    public Vector3 Position 
    {
        get { return transform.position; }
        private set { transform.position = value; }
    }

    public Quaternion Rotation 
    {
        get { return transform.rotation; }
        private set { transform.rotation = value; }
    }
    
    public void init(Vector3 initialPosition)
    {
        this.Position = initialPosition;
    }

    public float Speed { get; protected set; }

    // Awake is called before start
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    protected abstract void Run();
}

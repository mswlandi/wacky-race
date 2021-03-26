using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only used to detect collisions on plunger stick. The two flags are used by the Plunger script.
public class PlungerStick : MonoBehaviour
{
    public bool isAttached = false;
    public bool isAttachedToCar = false;
    public int botLayer = 11;
    public GameObject collidedObject;

    //Detect collisions between the GameObjects with Colliders attached
    private void OnTriggerEnter(Collider other)
    {
        isAttached = true;
        collidedObject = other.gameObject;

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collidedObject.layer == botLayer)
        {
            isAttachedToCar = true;
        }
    }
}

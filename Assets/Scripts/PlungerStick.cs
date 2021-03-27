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
    public bool collidedWithPlungerArm;

    //Detect collisions between the GameObjects with Colliders attached
    private void OnTriggerEnter(Collider other)
    {
        collidedObject = other.gameObject;

        if (collidedObject.tag == "SceneObject" || collidedObject.layer == botLayer || collidedObject.tag == "PlungerRotatableArm")
        {
            if (collidedObject.tag != "PlungerRotatableArm")
            {
                isAttached = true;
            }
            
            if (collidedObject.layer == botLayer)
            {
                isAttachedToCar = true;
            }
            else if (collidedObject.tag == "PlungerRotatableArm")
            {
                collidedWithPlungerArm = true;
            }
        }
    }
}

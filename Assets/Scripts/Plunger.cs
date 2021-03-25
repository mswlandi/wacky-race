using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{

    public Transform target;
    public float rotationSnapTime = 0.3F;

    private Transform lookAtVector;
    private float yVelocity = 0.0F;
    private float xVelocity = 0.0F;
    private Transform rotatableBase;
    private Transform rotatableArm;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rotatableBase = transform.GetChild(0);
        rotatableArm = rotatableBase.GetChild(0);

        targetRotation = Quaternion.LookRotation(target.transform.position - rotatableBase.position);
        rotatableBase.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(rotatableBase.eulerAngles.y, targetRotation.eulerAngles.y, ref yVelocity, rotationSnapTime), 0);

        targetRotation = Quaternion.LookRotation(rotatableArm.position - target.transform.position);
        rotatableArm.eulerAngles = new Vector3(Mathf.SmoothDampAngle(rotatableArm.eulerAngles.x, targetRotation.eulerAngles.x, ref xVelocity, rotationSnapTime), 180 + rotatableBase.eulerAngles.y, 0);
    }
}

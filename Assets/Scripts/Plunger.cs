using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{

    public Transform target;
    public RectTransform crosshair;
    public float rotationSnapTime = 0.2F;

    public float minScale = 0.4f; // dummy values are examples
    public float maxScale = 2f;
    public float maxScaleAtDistance = 0;
    public float minScaleAtDistance = 20;

    private Transform lookAtVector;
    private float yVelocity = 0.0F;
    private float xVelocity = 0.0F;
    private Transform rotatableBase;
    private Transform rotatableArm;
    private Quaternion targetRotation;

    private Bounds targetBounds;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region Look-At Rotations
        rotatableBase = transform.GetChild(0);
        rotatableArm = rotatableBase.GetChild(0);

        targetRotation = Quaternion.LookRotation(target.transform.position - rotatableBase.position);
        rotatableBase.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(rotatableBase.eulerAngles.y, targetRotation.eulerAngles.y, ref yVelocity, rotationSnapTime), 0);

        targetRotation = Quaternion.LookRotation(rotatableArm.position - target.transform.position);
        rotatableArm.eulerAngles = new Vector3(Mathf.SmoothDampAngle(rotatableArm.eulerAngles.x, targetRotation.eulerAngles.x, ref xVelocity, rotationSnapTime), 180 + rotatableBase.eulerAngles.y, 0);
        #endregion


        crosshair.position = Camera.main.WorldToScreenPoint(target.position);
        
        if (Camera.main.WorldToScreenPoint(target.position).z > 0)
        {
            float distance = Vector3.Distance(Camera.main.transform.position, target.position);
            float distance_t = Mathf.InverseLerp(minScaleAtDistance, maxScaleAtDistance, distance);
            float scale = Mathf.Lerp(minScale, maxScale, distance_t);

            crosshair.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            crosshair.localScale = new Vector3(0, 0, 0);
        }
    }

    public Vector2 positivateVec2(Vector2 v)
    {
        return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }

    public Vector2 vec2Div(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.x/v2.x, v1.y/v2.y);
    }

    // Get Bounds of an object and its children
    public static Bounds GetBounds(GameObject obj)
    {
        // Switch every collider to renderer for more accurate result
        Bounds bounds = new Bounds();
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();

        if (colliders.Length > 0)
        {
            //Find first enabled renderer to start encapsulate from it
            foreach (Collider collider in colliders)
            {
                if (collider.enabled) {
                    bounds = collider.bounds;
                    break;
                }
            }

            //Encapsulate (grow bounds to include another) for all collider
            foreach (Collider collider in colliders)
            {
                if (collider.enabled)
                {
                    bounds.Encapsulate(collider.bounds);
                }
            }
        }
        return bounds;
    }
}

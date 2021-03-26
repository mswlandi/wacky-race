using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridors : MonoBehaviour
{
    public float horizontalDistance;
    public float verticalDistance;

    private Vector3 firstPosition;

    private Car[] corridors;
    
    // Start is called before the first frame update
    void Start()
    {
        firstPosition = GetComponent<Transform>().position;
        corridors = GetComponentsInChildren<Car>();

        corridors[0].init(firstPosition);

        for (int i = 1; i < corridors.Length; i++)
        {
            if ((i % 2) == 1)
            {
                corridors[i].init(new Vector3(firstPosition.x + horizontalDistance, corridors[i-1].Position.y, corridors[i-1].Position.z - verticalDistance));
            }
            else
            {
                corridors[i].init(new Vector3(firstPosition.x, corridors[i-1].Position.y, corridors[i-1].Position.z - verticalDistance));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

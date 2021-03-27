using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Corridors : MonoBehaviour
{
    public float horizontalDistance;
    public float verticalDistance;

    private Vector3 firstPosition;

    private Player[] corridors;
    
    // Start is called before the first frame update
    void Start()
    {
        firstPosition = GetComponent<Transform>().position;
        corridors = GetComponentsInChildren<Player>();

        corridors[0].init(firstPosition, 0);

        for (int i = 1; i < corridors.Length; i++)
        {
            if ((i % 2) == 1)
            {
                corridors[i].init(new Vector3(firstPosition.x + horizontalDistance, corridors[i-1].Position.y, corridors[i-1].Position.z - verticalDistance), i);
            }
            else
            {
                corridors[i].init(new Vector3(firstPosition.x, corridors[i-1].Position.y, corridors[i-1].Position.z - verticalDistance), i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Array.Sort(corridors, Player.CompareTo);

        for (int i = 0; i < corridors.Length; i++)
        {
            corridors[i].RacePosition = i;
        }
    }
}

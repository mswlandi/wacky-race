 using UnityEngine;
 using System.Collections;
 
 public class Laps : MonoBehaviour {
     
     // These Static Variables are accessed in "checkpoint" Script
    public Checkpoints checkpoints;
    
    private Transform[] checkPointArray;
    public static Transform[] checkpointA;
    public static int currentCheckpoint = 0; 
    public static int currentLap = 0; 
    public Vector3 startPos;
    public int Lap;
    public int NextCheckpoint;
     
    void  Start ()
    {
        checkPointArray = checkpoints.Transforms;
        startPos = transform.position;
        currentCheckpoint = 0;
        currentLap = 0;
    }
 
    void  Update ()
    {
        Lap = currentLap;
        checkpointA = checkPointArray;
        NextCheckpoint = currentCheckpoint;
    }
     
}
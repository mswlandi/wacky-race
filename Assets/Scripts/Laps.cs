 using UnityEngine;
 using System.Collections;
 
 public class Laps : MonoBehaviour 
 {
    public Checkpoints checkpoints;
    
    private int currentCheckpoint;
    public int CurrentCheckpoint { get { return currentCheckpoint; } private set { currentCheckpoint = value; } }
    private int currentLap; 
    public int CurrentLap { get { return currentLap; } private set { currentLap = value; } }

    public int NextCheckpoint { get { return (currentCheckpoint + 1) % checkpoints.Size; } }

    public Transform[] CheckPointArray { get { return checkpoints.Transforms; } }
     
    void Start()
    {
        CurrentCheckpoint = 0;
        CurrentLap = 0;
    }

    public void IncrementCheckpoint()
    {
        if(NextCheckpoint == 1)
        {
            CurrentLap++;
        }
        CurrentCheckpoint = NextCheckpoint;
    }

    public Transform CurrentCheckpointTransform()
    {
        return CheckPointArray[CurrentCheckpoint];
    }

    public Transform NextCheckpointTransform()
    {
        return CheckPointArray[NextCheckpoint];
    }
}
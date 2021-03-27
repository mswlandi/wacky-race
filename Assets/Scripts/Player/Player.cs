using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Character character;
    public Laps Laps { get; private set; }
    public Energy Energy { get; private set; }
    public Vector3 Position { get { return character.Position; } }

    public int RacePosition { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInChildren<Character>();
        Laps = GetComponentInChildren<Laps>();
        Energy = GetComponentInChildren<Energy>();
    }

    public void init(Vector3 initialPosition, int racePosition)
    {
        this.character.init(initialPosition);
        this.RacePosition = racePosition;
    }

    public void IncrementEnergy(int increment)
    {
        this.Energy.value = this.Energy.value + increment;
    }

    public void DecrementEnergy(int decrement)
    {
        this.Energy.value = this.Energy.value - decrement;
    }

    public float DistanceToNextCheckpoint()
    {
        return Vector3.Distance(this.Position, this.Laps.CurrentCheckpointTransform().position);
    }

    public static int CompareTo(Player player, Player other)
    {
        int response;
        if (player.Laps.CurrentLap != other.Laps.CurrentLap)
        {
            response = other.Laps.CurrentLap - player.Laps.CurrentLap;
        }
        else if (player.Laps.CurrentCheckpoint != other.Laps.CurrentCheckpoint)
        {
            response = other.Laps.CurrentCheckpoint - player.Laps.CurrentCheckpoint;
        }
        else
        {
            response = (int)(player.DistanceToNextCheckpoint() - other.DistanceToNextCheckpoint());
        }

        return response;
    }
}

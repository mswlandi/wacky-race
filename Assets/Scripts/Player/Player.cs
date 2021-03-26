using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Character character;
    public Laps Laps { get; private set; }
    public Energy Energy { get; private set; }
    public Vector3 Position { get { return character.Position; } }
    
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInChildren<Character>();
        Laps = GetComponentInChildren<Laps>();
        Energy = GetComponentInChildren<Energy>();
    }

    public void IncrementEnergy(int increment)
    {
        this.Energy.value = this.Energy.value + increment;
    }
}

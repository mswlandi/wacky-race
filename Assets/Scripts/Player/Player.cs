using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Character character;
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

        int playerCurrentLap = player.Laps.CurrentLap;
        int otherCurrentLap = other.Laps.CurrentLap;

        if (player.Laps.NextLap)
        {
            playerCurrentLap++;
        }

        if (other.Laps.NextLap)
        {
            otherCurrentLap++;
        }


        if (playerCurrentLap != otherCurrentLap)
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

    public void EndGame()
    {
        this.Laps.CurrentLap = 0;
        
        Color transp = new Color(0f, 0f, 0f, 0f);
        Color white = new Color(1f, 1f, 1f, 1f);
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Play("Win");
        foreach(Transform ps in FindObjectOfType<Canvas>().GetComponentsInChildren<Transform>()) {
            if (ps.gameObject.name == "Position") ps.GetComponent<UnityEngine.UI.Text>().color = transp;
            if (ps.gameObject.name == "PositionFinal")  
            {
                ps.GetComponent<UnityEngine.UI.Text>().color = white;
                ps.GetComponent<UIPosition>().enabled = false;
            }
        }
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("Menu");
    }
}

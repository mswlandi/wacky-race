 using UnityEngine;
 using System.Collections;
 
 public class Checkpoint : MonoBehaviour 
 {
    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if(player != null)
        {
            if(transform == player.Laps.CheckPointArray[player.Laps.CurrentCheckpoint].transform) 
            {
                // Hide the last checkpoint for player and show the next one
                if(player.CompareTag("Player"))
                {
                    Renderer currentRenderer = player.Laps.CheckPointArray[player.Laps.CurrentCheckpoint].GetComponentInChildren<Renderer>();
                    Renderer nextRenderer = player.Laps.CheckPointArray[player.Laps.NextCheckpoint].GetComponentInChildren<Renderer>();
                    if(currentRenderer != null && nextRenderer != null)
                    {
                        currentRenderer.enabled = false;
                        nextRenderer.enabled = true;
                    }
                }
                
                player.Laps.IncrementCheckpoint();

                if(player.Laps.CurrentLap == 4 && player.CompareTag("Player")) player.EndGame();
            }
        }
    }
}
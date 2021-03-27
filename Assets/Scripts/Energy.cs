 using UnityEngine;
 using System.Collections;
 
 public class Energy : MonoBehaviour {
    public int value;
 
    void Start()
    {
        StartCoroutine(addEnergy());
    }
    
    IEnumerator addEnergy()
    {
        while (true)
        { 
            if (value < 0)
            {
                value = 0;
            }
            else if (value < 100)
            {
                value += 1;
                yield return new WaitForSeconds(1);
            } 
            else 
            {
                value = 100;
                yield return null;
            }
        }
    }

 }
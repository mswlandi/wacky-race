 using UnityEngine;
 using System.Collections;
 
 public class Energy : MonoBehaviour {
    public static int curEnergy = 0;
    public EnergyBar energyBar;
 
    void Start()
    {
        StartCoroutine(addEnergy());
        energyBar.SetEnergy(curEnergy);
    }
    
    IEnumerator addEnergy()
    {
        while (true){ 
            if (curEnergy < 100){
                curEnergy += 1;
                yield return new WaitForSeconds(1);
            } else {
                curEnergy = 100;
                yield return null;
            }
            energyBar.SetEnergy(curEnergy);
        }
    }

 }
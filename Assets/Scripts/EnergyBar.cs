 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;

 public class EnergyBar : MonoBehaviour {
     
     public Slider slider;

     public void SetEnergy(int energy)
     {
        slider.value = energy;
     }
 }
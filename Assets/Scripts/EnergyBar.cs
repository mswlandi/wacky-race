 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;

 public class EnergyBar : MonoBehaviour {
     
    public Slider slider;
    private Energy energy;

    public void Start()
    {
        energy = GetComponent<Energy>();
    }
    
    public void Update()
    {
        SetSlider(energy.value);
    }

    public void SetSlider(int value)
    {
        slider.value = value;
    }
 }
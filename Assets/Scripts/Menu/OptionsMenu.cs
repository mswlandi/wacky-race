using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : Menu
{
    public Slider slider;
    public RectTransform imageTransform;

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    private float oldValue;

    private Resolution[] resolutions;

    private void Start() {
        oldValue = slider.minValue;

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<String> options = new List<String>();
        int currentResolutionIndex = 0;
        int counter = 0;

        foreach (Resolution resolution in resolutions)
        {
            options.Add(resolution.width + "x" + resolution.height);
            
            if(resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = counter;
            }

            counter++;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void RotateSliderHandleImage(float value) {
        value = Math.Min(Math.Max(slider.minValue, value), slider.maxValue);

        imageTransform.Rotate(new Vector3(0, 0, value*Math.Sign(value - oldValue)));
        oldValue = value;
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("Volume", value);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

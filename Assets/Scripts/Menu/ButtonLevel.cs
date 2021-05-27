using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class ButtonLevel : MonoBehaviour
{
    private Race race;

    private MenuCameraController cameraController;
    public String levelName;

    public Camera camera;

    private void Start() {
        race = (Race) FindObjectOfType(typeof(Race));
        cameraController = (MenuCameraController) FindObjectOfType(typeof(MenuCameraController));
    }
    public void SetLevelScene()
    {
        switch (levelName.ToLower())
        {
            case "nature": 
                race.LevelScene = Race.Levels.Nature;
                break;
            case "nascar": 
                race.LevelScene = Race.Levels.Nascar;
                break;
            default:
                race.LevelScene = Race.Levels.Nature;
                break;
        }

        if (camera != null)
            cameraController.CurrentCamera = camera;
    }
}

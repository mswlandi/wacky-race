using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ButtonCharacter : MonoBehaviour
{
    private Race race;
    public String characterName;
    private MenuCameraController cameraController;
    public Camera natureCamera;
    public Camera nascarCamera;

    private void Start() {
        race = (Race) FindObjectOfType(typeof(Race));
        cameraController = (MenuCameraController) FindObjectOfType(typeof(MenuCameraController));
    }

    public void SetPlayerCharacter() {

        switch (characterName.ToLower())
        {
            case "booster": 
                race.Character = Race.Characters.Booster;
                break;
            case "discombobulate": 
                race.Character = Race.Characters.Discombobulate;
                break;
            case "riddikulus": 
                race.Character = Race.Characters.Riddikulus;
                break;
            default:
                race.Character = Race.Characters.Booster;
                break;
        }

        if (race.LevelScene.Value == Race.Levels.Nascar.Value) {
            cameraController.CurrentCamera = nascarCamera;
        } else {
            cameraController.CurrentCamera = natureCamera;
        }
    }

}

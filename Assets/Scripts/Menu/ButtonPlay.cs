using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPlay : ButtonFunction
{    
    public Image wheelImage;
    public override void Run()
    {
        SaveContext();

        List<MenuButton> childButtons = GetComponentsInChildren<MenuButton>().ToList();
        childButtons.RemoveAt(0);
        MenuButtonController menuButtonController = GetComponentInParent<MenuButtonController>();
        menuButtonController.index = 0;
        menuButtonController.maxIndex = childButtons.Count - 1;
        menuButtonController.keyDown = false;

        foreach (MenuButton button in oldMenuContext.buttons)
        {
            button.Activated = false;
        }

        foreach (MenuButton button in childButtons)
        {
            button.Activated = true;
            button.menuButtonController = menuButtonController;
        }

        MenuShowCameraController menuCameraController = GetComponentInChildren<MenuShowCameraController>();
        menuCameraController.activated = true;

        wheelImage.enabled = false;
    }
}

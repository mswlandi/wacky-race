using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ButtonFunction : MonoBehaviour
{
    public abstract void Run();

    public struct MenuContext
    {
        public MenuButtonController buttonController;
        public List<MenuButton> buttons;
    }

    protected MenuContext oldMenuContext;
    public MenuButtonController currentButtonController;

    void Start()
    {
        oldMenuContext.buttonController = new MenuButtonController();
    }

    protected void SaveContext()
    {
        oldMenuContext.buttonController.index = currentButtonController.index;
        oldMenuContext.buttonController.keyDown = currentButtonController.keyDown;
        oldMenuContext.buttonController.maxIndex = currentButtonController.maxIndex;

        GameObject goCurrentContext = currentButtonController.gameObject;
        oldMenuContext.buttons = goCurrentContext.GetComponentsInChildren<MenuButton>().Where(button => button.Activated).ToList();
    }

    protected void LoadContext()
    {
        currentButtonController.index = oldMenuContext.buttonController.index;
        currentButtonController.keyDown = oldMenuContext.buttonController.keyDown;
        currentButtonController.maxIndex = oldMenuContext.buttonController.maxIndex;
        currentButtonController.audioSource = oldMenuContext.buttonController.audioSource;

        GameObject goCurrentContext = currentButtonController.gameObject;
        List<MenuButton> currentButtons = goCurrentContext.GetComponentsInChildren<MenuButton>().ToList().FindAll(button => button.Activated);

        foreach (MenuButton button in currentButtons)
        {
            button.Activated = false;
        }
        
        foreach (MenuButton button in oldMenuContext.buttons)
        {
            button.Activated = true;
        }
    }
}

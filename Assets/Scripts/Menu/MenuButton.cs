using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
	[SerializeField] public MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;
	[SerializeField] ButtonFunction function;
	[SerializeField] bool activated;

	public int Index { get { return thisIndex; } }
	public ButtonFunction Function { get { return function; } }
	public bool Activated { 
		get { return activated; } 
		set 
		{
			activated = value;

			Image[] images = GetComponentsInChildren<Image>();
			Text[] texts = GetComponentsInChildren<Text>();

			foreach (Image image in images)
			{
				image.enabled = activated;
			}

			foreach (Text text in texts)
			{
				text.enabled = activated;
			}
		}
	}
	
	// Update is called once per frame
    void Update()
    {
		if(Activated)
		{
			if(menuButtonController.index == thisIndex)
			{
				animator.SetBool ("Selected", true);
				if(Input.GetAxis ("Submit") == 1){
					animator.SetBool ("Pressed", true);
					if (function != null)
					{
						function.Run();
					}
				}else if (animator.GetBool ("Pressed")){
					animator.SetBool ("Pressed", false);
					animatorFunctions.disableOnce = true;
				}
			}else{
				animator.SetBool ("Selected", false);
			}
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;
	[SerializeField] ButtonFunction function;

	public int Index { get { return thisIndex; } }
	public ButtonFunction Function { get { return function; } }

    // Update is called once per frame
    void Update()
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

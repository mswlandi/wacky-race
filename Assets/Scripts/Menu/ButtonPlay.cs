using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : ButtonFunction
{
    public override void Run()
    {
        SceneManager.LoadScene("Track1");
    }
}

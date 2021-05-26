using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity;

public class Race : MonoBehaviour
{
    public String levelSceneName;
    public String characterName;

    private void Update() {
        if(levelSceneName.Length != 0 && characterName.Length != 0)
        {
            LoadLevel();
            levelSceneName = "";
            characterName = "";
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelSceneName + characterName);
    }
}

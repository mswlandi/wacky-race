using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPlay : MonoBehaviour
{
    private Race race;

    private void Start() {
        race = (Race) FindObjectOfType(typeof(Race));
    }

    public void PlayButton ()
    {
        race.LoadLevel();
    }
}

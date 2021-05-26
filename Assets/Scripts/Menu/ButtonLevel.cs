using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class ButtonLevel : ButtonFunction
{
    public String levelName;
    public override void Run()
    {
        Race race = GetComponentInParent<Race>();

        race.levelSceneName = levelName;
    }
}

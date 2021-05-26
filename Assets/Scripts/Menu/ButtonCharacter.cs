using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ButtonCharacter : ButtonFunction
{
    public String characterName;
    public override void Run()
    {
        Race race = GetComponentInParent<Race>();

        race.characterName = characterName;
    }

}

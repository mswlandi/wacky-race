using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity;

public class Race : MonoBehaviour
{
    public class Levels
    {
        private Levels(string value) { Value = value; }

        public string Value { get; private set; }

        public static Levels Nature { get { return new Levels("Nature"); } }
        public static Levels Nascar { get { return new Levels("Nascar"); } }
    }

    public class Characters
    {
        private Characters(string value) { Value = value; }

        public string Value { get; private set; }

        public static Characters Booster { get { return new Characters("Booster"); } }
        public static Characters Discombobulate { get { return new Characters("Discombobulate"); } }
        public static Characters Riddikulus { get { return new Characters("Riddikulus"); } }
    }

    [SerializeField]
    private Levels levelScene = Levels.Nature;
    public Levels LevelScene { get; set; }
    
    [SerializeField]
    private Characters character = Characters.Booster;
    public Characters Character { get; set; }

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelScene.Value + Character.Value);
    }
}

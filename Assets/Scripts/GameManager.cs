using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputController InputController { get; private set; }
    
    // Awake is called before start
    void Awake()
    {
        Instance = this;
        InputController = GetComponentInChildren<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

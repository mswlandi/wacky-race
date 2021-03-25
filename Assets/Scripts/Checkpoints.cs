using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Transform[] Transforms { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Transforms = GetComponentsInChildren<Transform>();
        Transforms = Transforms.Skip(1).ToArray();
        Debug.Log(Transforms[0].position);
        Debug.Log(Transforms[1].position);
        Debug.Log(Transforms[2].position);
    }
}

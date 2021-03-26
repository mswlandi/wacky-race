using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Transform[] Transforms { get; private set; }
    public int Size { get { return Transforms.Length; }}
    
    // Start is called before the first frame update
    void Start()
    {
        Transforms = GetComponentsInChildren<Transform>();
        Transforms = Transforms.Skip(1).ToArray();
    }
}

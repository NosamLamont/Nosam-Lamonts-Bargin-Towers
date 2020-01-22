﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    public Node node;
    public bool walkable; 
    public bool constructable; 

    public EnvironmentObject()
    {
        
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        //y is always zero sinces its meant to be 2d plane grid.
        node = new Node(true, true, new Vector3(transform.position.x, -1, transform.position.z));

    }
}

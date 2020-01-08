using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool walkable;
    public bool constuctable;
    public Vector3 worldPosition;

    public Node(bool _walkable, bool _constuctable, Vector3 _worldPos)
    {
        walkable = _walkable;
        constuctable = _constuctable;
        worldPosition = _worldPos;
    }
}


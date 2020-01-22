using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingGrid
{
    public List<Node> Grid = new List<Node>();
    public Vector2 GridSize = new Vector2();
    public int DefaultHeight = 0;

    public Node NodeFromWroldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + GridSize.x/2) / GridSize.x;
        float percentY = (worldPosition.z + GridSize.y/2) / GridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((GridSize.x) * percentX);
        int y = Mathf.RoundToInt((GridSize.y) * percentY);
        Vector3 RoundedVector = new Vector3(x, DefaultHeight, y);
        Node nodeToReturn = Grid.Find(n => n.worldPosition == RoundedVector);  //Find the 
                
        return nodeToReturn;
    }
}


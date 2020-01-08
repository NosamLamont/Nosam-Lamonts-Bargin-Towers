using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Vector2 gridWroldSize;
    public float nodeRadius;
    public LayerMask impassableMask;
    public Transform unit;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    Node[,] grid;

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWroldSize.x, 1, gridWroldSize.y));
        if(grid != null)
        {
            Node unitNode = NodeFromWroldPoint(unit.position);
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable)?Color.white:Color.red;
                if(unitNode == n)
                {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }

    }
    private void Awake() {
        GameObject obj = this.gameObject;
        obj.transform.position = new Vector3(0, 1.0f, 0);
    }

    private void Start() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWroldSize.x/nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWroldSize.y/nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottemLeft = transform.position - Vector3.right * gridWroldSize.x/2 - Vector3.forward * gridWroldSize.y/2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                 Vector3 worldPoint = worldBottemLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                //bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, impassableMask));
                 grid[x,y] = new Node(true, true ,worldPoint);
            }
        }
    }

    public Node NodeFromWroldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWroldSize.x/2) / gridWroldSize.x;
        float percentY = (worldPosition.z + gridWroldSize.y/2) / gridWroldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x,y];

    }
}

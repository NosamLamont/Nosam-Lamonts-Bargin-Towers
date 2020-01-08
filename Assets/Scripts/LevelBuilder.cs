using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public string LevelName;
    public float buildableAreaDefaultHeight;
    public float enemyPathDefaultHeight;
    public float enemyWaypointDefaultHeight;
    public float enemyStartDefaultHeight;
    public float enemyEndDefaultHeight;

    public Material wallMat;
    public Material enemyPathMat;
    public Material enemyWaypointMat;
    public Material enemyStartMat;
    public Material enemyFinishMat;

    public Transform testUnit;

    public Level newLevel;
    
    public List<Node> pathfindingGrid;

    public Vector2 GridSize;
    
    private void Awake()
    {
        //Get Pixel Data from image --> Name of the image(png) you want to use 
        LevelName = "SpiralCombo";
        wallMat = (Material)Resources.Load("Materials/Ground");
        enemyPathMat = (Material)Resources.Load("Materials/Path");
        buildableAreaDefaultHeight = 1f;
        enemyPathDefaultHeight = 0f;
        enemyWaypointDefaultHeight = 0.5f;
        enemyStartDefaultHeight = 1.0f;
        enemyEndDefaultHeight = 1.0f;
        newLevel = new Level();
        pathfindingGrid = new List<Node>();
        BuildLevel(LevelName);
    }

    private void BuildLevel(string LevelName)
    {
        try
        {
            Texture2D Image = (Texture2D)Resources.Load("Level Images/" + LevelName);
            newLevel = pixelReader(Image, newLevel);
            BuildMaze(newLevel);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void BuildMaze(Level newLevel)
    {
        Mesh myMesh = new Mesh();

        GameObject objWalls = new GameObject("Walls");
        objWalls.AddComponent<MeshFilter>();
        objWalls.AddComponent<MeshRenderer>();

        GameObject objEnemyPaths = new GameObject("EnemyPaths");
        objEnemyPaths.AddComponent<MeshFilter>();
        objEnemyPaths.AddComponent<MeshRenderer>();

        GameObject objEnemyWaypoints = new GameObject("EnemyWaypoints");
        objEnemyWaypoints.AddComponent<MeshFilter>();
        objEnemyWaypoints.AddComponent<MeshRenderer>();

        foreach(var position in newLevel.buildableArea)
        {
            //check if it it has at least two items
            if(position != null)
            {
                HelperScript.LoadPrefabAtPosition("Wall",position, objWalls);
            }
        }
        
        foreach(var position in newLevel.enemyPath)
        {
            //check if it it has at least two items
            if(position != null)
            {
                HelperScript.LoadPrefabAtPosition("EnemyPath", position, objEnemyPaths);
            }
        }

        foreach(var position in newLevel.waypoints)
        {
            //check if it it has at least two items
            if(position != null)
            {
                //raise a little
                HelperScript.LoadPrefabAtPosition("EnemyWaypoint", position, objEnemyWaypoints);
            }
        }

        //find the way points that are grouped together to form 1 way point. Maybe group the meshes together
        HelperScript.LoadPrefabAtPosition("EnemyStartPoint", HelperScript.CenterOfVectors(newLevel.start));
        HelperScript.LoadPrefabAtPosition("EnemyEndPoint", HelperScript.CenterOfVectors(newLevel.end));
        
        pathfindingGrid = AddObjectNodesToPathfindingGrid();
        GridSize = HelperScript.FindGridSize(pathfindingGrid);

        //Might want this later? I dunno man
        //HelperScript.CombineChildMeshes(objEnemyPaths, enemyPathMat);
        //organiseWaypoints(objEnemyWaypoints);
    }

    private Level pixelReader(Texture2D Image, Level newLevel)
    {
        for (int i = 0; i < Image.width; i++)
        {
            for (int j = 0; j < Image.height; j++)
            {
                Color pixel = Image.GetPixel(i,j);

                if(pixel == Color.black)
                {
                    newLevel.buildableArea.Add(new Vector3(i, buildableAreaDefaultHeight, j));
                }
                else if (pixel == Color.blue) 
                {
                    newLevel.enemyPath.Add(new Vector3(i, enemyPathDefaultHeight, j));
                }
                // Yellow
                else if (pixel.r == 1.0 && pixel.g == 1.0 && pixel.a == 1.0)
                {
                    newLevel.waypoints.Add(new Vector3(i, enemyWaypointDefaultHeight, j));
                    newLevel.enemyPath.Add(new Vector3(i, enemyPathDefaultHeight, j));
                }
                else if (pixel == Color.green)
                {
                    newLevel.start.Add(new Vector3(i, enemyStartDefaultHeight, j));
                    newLevel.enemyPath.Add(new Vector3(i, enemyPathDefaultHeight, j));
                }
                else if (pixel == Color.red)
                {
                    newLevel.end.Add(new Vector3(i, enemyEndDefaultHeight, j));
                    newLevel.enemyPath.Add(new Vector3(i, enemyPathDefaultHeight, j));
                }
                else
                {
                    newLevel.nonBuildableArea.Add(new Vector3(i, buildableAreaDefaultHeight, j));
                }
            }
        }
        return newLevel;
    }

    //Adds objects to the pathfinding grid
    private List<Node> AddObjectNodesToPathfindingGrid()
    {
        List<Node> Grid = new List<Node>();
        Wall[] objects = GameObject.FindObjectsOfType<Wall>();
        Path[] paths = GameObject.FindObjectsOfType<Path>();
        Debug.Log(objects.Length);
        if(objects != null)
        {
            foreach(Wall obj in objects)
            {
                Grid.Add(obj.gameObject.GetComponent<Wall>().node);
            }
        }

        if(paths != null)
        {
            foreach(Path obj in paths)
            {
                Grid.Add(obj.gameObject.GetComponent<Path>().node);
            }  
        }
        return Grid;
    }

    private void OnDrawGizmos() {
        if(newLevel != null)
        {
            foreach(Node n in pathfindingGrid)
            {
                Gizmos.color = (n.walkable)?Color.white:Color.red;
                Gizmos.DrawCube(n.worldPosition,new Vector3(1f,0.1f,1f));
            }
        }

    }


}

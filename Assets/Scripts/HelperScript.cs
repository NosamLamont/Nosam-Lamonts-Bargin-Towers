﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScript : MonoBehaviour
{
    ///Creates a Prefab at Position
    public static void LoadPrefabAtPosition(string PrefabName, Vector3 Position)
    {
        GameObject prefab = (GameObject)Instantiate(Resources.Load("Prefabs/" + PrefabName), Position, new Quaternion());
    }

    ///Creates a Prefab Named at Position and adds it to a specified Parent gameojbect
    public static void LoadPrefabAtPosition(string PrefabName, Vector3 Position, GameObject Parent)
    {
        GameObject prefab = (GameObject)Instantiate(Resources.Load("Prefabs/" + PrefabName), Position, new Quaternion());
        prefab.transform.parent = Parent.transform;
    }

    public static GameObject ReturnLoadPrefabAtPosition(string PrefabName, Vector3 Position)
    {
        GameObject prefab = (GameObject)Instantiate(Resources.Load("Prefabs/" + PrefabName), Position, new Quaternion());
        return prefab;
    }

    public static void LoadPrefabAtPosition(string PrefabName, float x, float y, float z)
    {
        GameObject prefab = (GameObject)Instantiate(Resources.Load("Prefabs/" + PrefabName), new Vector3(x,y,z), new Quaternion());
    }

    ///Combines a Game Object's children into one mesh
    public static void CombineChildMeshes(GameObject obj, Material mat)
    {
        if(obj.transform.GetComponent<MeshFilter>() == null)
        {
            obj.gameObject.AddComponent<MeshFilter>();
        }
        Vector3 position = obj.transform.position;
        obj.transform.position = Vector3.zero;

        //Get all mesh filters and combine
        MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        Debug.Log(combine.Length);
        int i = 1;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        obj.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true, true);
        obj.transform.gameObject.SetActive(true);

        if(obj.transform.GetComponent<Renderer>() == null)
        {
            obj.AddComponent<Renderer>();
        }        
        obj.GetComponent<Renderer>().material = mat;

        //Return to original position
        obj.transform.position = position;
        obj.transform.position = position;

    }

    /// Returns a vector that represents the center of a bunch of vectors
    public static Vector3 CenterOfVectors(List<Vector3> Vectors)
    {
        Vector3 sum = Vector3.zero;
        if(Vectors == null || Vectors.Count == 0)
        {
            return sum;
        }

        foreach(Vector3 vec in Vectors)
        {
        //     Debug.Log(vec);
            sum += vec;
        }
        return sum/Vectors.Count;
    }

    // public static Vector2 FindMinMaxFromVector3(List<Vector3> vectors)
    // {
    //     float? min = null;
    //     float? max = null;

    //     foreach(Vector3 vec in vectors)
    //     {
    //         if(min != null && max != null)
    //         {
    //             if(min < vec.x)
    //         }
    //     }
    //     return new Vector2(0,0);
    // }
    
}

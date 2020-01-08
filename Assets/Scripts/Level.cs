using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level
{
   public List<Vector3> buildableArea {get; set;}
   public List<Vector3> nonBuildableArea {get; set;}
   public List<Vector3> enemyPath {get; set;}
   public List<Vector3> waypoints {get; set;}
   public List<Vector3> start {get; set;}
   public List<Vector3> end {get; set;}

   public Level()
   {
       buildableArea = new List<Vector3>();
       nonBuildableArea = new List<Vector3>();
       enemyPath = new List<Vector3>();
       waypoints = new List<Vector3>();
       start = new List<Vector3>();
       end = new List<Vector3>();
   }
}

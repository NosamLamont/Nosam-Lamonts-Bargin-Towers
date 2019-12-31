using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;
    private Transform nextWaypoint;
    private int wavepointIndex = 0;
    public bool LoopWaypoints = true;

    void Start()
    {
        nextWaypoint = Waypoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = nextWaypoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, nextWaypoint.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {


        if (Waypoints.points.Length >= wavepointIndex)
        {
            wavepointIndex++;
            nextWaypoint = Waypoints.points[wavepointIndex];
        }
        else
        {
            wavepointIndex = 0;
            nextWaypoint = Waypoints.points[wavepointIndex];
        }
    }
}

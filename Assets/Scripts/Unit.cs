using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isMoveable { get; set; }
    public bool isTargetable { get; set; }
    public bool hasTarget { get; set; }
    public bool canShoot { get; set; }
    public float attackCooldown { get; set; }

    void Start()
    {
        isTargetable = true;
        isMoveable = false;
        canShoot = true;
        attackCooldown = 1.0f;
    }
}

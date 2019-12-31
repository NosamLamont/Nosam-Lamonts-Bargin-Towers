using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{
    public float attackCooldownCount;
    public GameObject nearestEnemy;
    public Collider[] ObjectsWithinRadius;
    public float attackRange;
    // Start is called before the first frame update
    void Start()
    {
        isMoveable = false;
        isTargetable = true;
        attackCooldown = 2f;
        attackRange = 10f;
        attackCooldownCount = Time.time + attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //Get objects within attack range
        ObjectsWithinRadius = Physics.OverlapSphere(this.gameObject.transform.position, attackRange);
        //Find the nearest Enemy
        nearestEnemy = FindNearestEnemey(ObjectsWithinRadius);
        //Shoot if there is an enemy and cooldown has subsided.
        if(nearestEnemy != null)
        {
            if (attackCooldownCount <= Time.time)
            {
                attackCooldownCount = Time.time + attackCooldown;
                Shoot(nearestEnemy);
            }
        }
    }

    private void Shoot(GameObject target)
    {
        //Create Projectile -> Gameobject projetile = (GameObject)Instantiate(pelletPrefab, shootPosition.position, shootPosition.rotation);
        //Projectile projectile
        Debug.Log("Shoot");
    }

    private GameObject FindNearestEnemey(Collider[] objectsWithinRadius)
    {
        GameObject target = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (Collider collider in objectsWithinRadius)
        {
            if (collider.gameObject.GetComponent<Enemy>() != null)
            {
                Vector3 directionToTarget = collider.gameObject.transform.position - transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    target = collider.gameObject;
                }
            }
        }
        return target;
    }
}

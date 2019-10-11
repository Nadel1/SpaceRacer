using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpecs : MonoBehaviour
{
    public float maxAngle;
    public float fireRate;
    public float capacity;

    public GameObject projectile;
    public GameObject shootFrom;
    public float projectileSpeed;
    public float range;
    

    public float getMaxAngle()
    {
        return maxAngle;
    }

    public float getFireRate()
    {
        return fireRate;
    }

    public float getCapacity()
    {
        return capacity;
    }
    public float getProjectileSpeed()
    {
        return projectileSpeed;
    }
    public float getRange()
    {
        return range;
    }

    public GameObject getProjectile()
    {
        return projectile;
    }

    public GameObject getShootPoint()
    {
        return shootFrom;
    }
}

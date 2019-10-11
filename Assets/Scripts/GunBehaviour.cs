﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunBehaviour : MonoBehaviour
{

    public Image Crosshair;
    private float xRot;
    private float yRot;

    private float maxAngle;

    private GameObject projectile;
    private GameObject shootFrom;
    private float projectileSpeed;
    private float range;

    private float fireRate;

    private float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        nextFire = 0;
        maxAngle = GetComponentInChildren<GunSpecs>().getMaxAngle();
        projectile = GetComponentInChildren<GunSpecs>().getProjectile();
        shootFrom = GetComponentInChildren<GunSpecs>().getShootPoint();
        projectileSpeed = GetComponentInChildren<GunSpecs>().getProjectileSpeed();
        fireRate = GetComponentInChildren<GunSpecs>().getFireRate();
        range = GetComponentInChildren<GunSpecs>().getRange();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotate();
        Shoot();
    }
    void Rotate()
    {
        xRot = Input.GetAxis("GunX") * maxAngle;
        yRot = Input.GetAxis("GunY") * maxAngle;
        Quaternion target = Quaternion.Euler(xRot, yRot, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);

    }

    void Shoot()
    {
        if (Input.GetButton("Shoot") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject shot = Instantiate(projectile, shootFrom.transform.position, gameObject.transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(transform.forward*projectileSpeed);
            Destroy(shot, range);
        }
    }

    void MoveCrosshair()
    {

    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectExplosion : MonoBehaviour
{
    public GameObject camera;
    public bool explosion;
    private Vector3 explosionOrigin;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Ship").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (explosion)
        {
            float distance = Vector3.Distance(explosionOrigin, playerPos);
            camera.GetComponent<CameraShake>().ShakeIt(distance);
            explosion = false;
        }
    }
    public void setExplosion(bool expl)
    {
        explosion = expl;
    }
    public void setExplosionOrigin(Vector3 pos)
    {
        explosionOrigin = pos;
    }
}

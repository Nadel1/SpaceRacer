﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShipController : MonoBehaviour
{
    public Text velocityText;
    private float xRot;
    private float yRot;

    private Rigidbody rb;
    private float vel;


    private float forwardVel;
    private float slowDownTo=0;
    private float boost=0;
    private float boostAmount=10;
    private float actualBoost;
    private float engineHealth=100.0f;
    public bool boosted=false;
    private float boostFactor;


    private GameObject engineSlot;
    //engine specs
    private float moveSpeed;
    private float stopSpeed;
    private float maxRotSpeed;
    private float minRotSpeed;
    private float maxSpeed;
    private float maxTilt;


    private float boostStopSpeed;
    private float rotationSpeed;
    private float component;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        engineSlot = GameObject.Find("EngineSlot");
        //get the attributes from the engine compartment
        moveSpeed = engineSlot.GetComponentInChildren<EngineSpecs>().getMoveSpeed();
        stopSpeed = engineSlot.GetComponentInChildren<EngineSpecs>().getStopSpeed();
        maxRotSpeed = engineSlot.GetComponentInChildren<EngineSpecs>().getMaxRotSpeed();
        minRotSpeed = engineSlot.GetComponentInChildren<EngineSpecs>().getMinRotSpeed();
        maxSpeed = engineSlot.GetComponentInChildren<EngineSpecs>().getMaxSpeed();
        maxTilt = engineSlot.GetComponentInChildren<EngineSpecs>().getMaxTilt();

        slowDownTo = 0;
        boostStopSpeed = stopSpeed/2;
       component = (float)Math.Pow(minRotSpeed / maxRotSpeed, 1 / maxSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Rotate();

        //moves ship forward until while clamping the velocity (hindering inifinte velocity)
        if (Input.GetButton("Move")&&forwardVel<maxSpeed)
        {
            forwardVel += moveSpeed;
            slowDownTo = forwardVel;
        }
        if (Input.GetButton("Boost")&&forwardVel>0)
        {
            CalculateBoost();
            
            DamageEngine();
            //Debug.Log(" Goal " + slowDownTo + " is " + forwardVel);
        }
        //stops ship without turning it around
        if (Input.GetButton("stop")&&Vector3.Dot(transform.forward,rb.velocity)>0)
        {

            forwardVel -= stopSpeed;
        }
        
        if (boosted && Vector3.Dot(transform.forward, rb.velocity) > 0 && forwardVel > slowDownTo)
        {
            if (!Input.GetButton("Boost"))
            {
                forwardVel -= stopSpeed;
            }
            
        }
        if (Vector3.Dot(transform.forward, rb.velocity) <= 0||forwardVel <= slowDownTo)
        {
            boosted = false;
            slowDownTo = forwardVel;
            boost= 0;
        }
        //clamp the velocity for the display
        forwardVel = Mathf.Clamp(forwardVel, 0, 2500);
        //actually moves the ship
        rb.velocity = transform.forward * forwardVel;
            //display current velocity
            velocityText.text = forwardVel.ToString("0.0");
        

        
    }
    void CalculateBoost()
    {
        //actualBoost = ( maxSpeed/ forwardVel) * (forwardVel / boostAmount) * engineHealth;
        //boostFactor makes it so that boosting at lower velocity gives you a bigger boost than boosting
        //on high velocity
        boostFactor = -5/maxSpeed * (slowDownTo)+5;
       

        //the boostFactor is neglected once maxSpeed got reached at the beginning of the boost
        actualBoost =  (boostFactor+1)*(boostAmount) / (10 * boostAmount * (forwardVel - slowDownTo) + 1)*(engineHealth/100);

        forwardVel += actualBoost;

        boost += actualBoost;

        boosted = true;

    }

    void DamageEngine()
    {
       // engineHealth -= slowDownTo / 2000000;
        engineHealth -= 1 / 20;
        
        //different damage wether boost is over the maxspeed or not
        if (forwardVel > maxSpeed)
        {
            maxSpeed -= engineHealth/1000;
        }
        else
        {
            maxSpeed -= engineHealth/100000000;
        }
        Debug.Log(maxSpeed);
        //update slowDownTo so that it´s the current max value and not the max value from before the boost
        if (slowDownTo > maxSpeed)
        {
            slowDownTo = maxSpeed;
        }
       
    }
  
    private void Rotate()
    {
        //rotation at medium speed is the highest
        float factor = (float)Math.Sin(((2 * Math.PI) / (2 * maxSpeed)) * (double)forwardVel);
        factor = Mathf.Clamp(factor, 0.28f, 0.9f);


        rotationSpeed =   factor*maxRotSpeed ;
        //prevent back rotation on x and y axis
        xRot += Input.GetAxis("Vertical") * rotationSpeed;
        yRot += Input.GetAxis("Horizontal") * rotationSpeed;
        //rotation on z axis depends on velocity
        float zRot;
        zRot = -Input.GetAxis("Horizontal") * factor * (maxTilt);
        if (forwardVel > 0.5f)//no tilt at 0 
        {
            //(maxSpeed / forwardVel)
            zRot = -Input.GetAxis("Horizontal") * factor * (maxTilt);
        }
        else
        {
            zRot = 0;
        }
       
        Quaternion target = Quaternion.Euler(xRot, yRot, zRot);
        //smooth rotation that rotates back (z axis) when no user input 
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
    }

    public float getForwardVel()
    {
        return forwardVel;
    }

    public float getMaxVel()
    {
        return maxSpeed;
    }

  
}


/*
 * private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        float y = Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed);
        float z = Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed);

        rb.velocity = new Vector3(x, y, z);
    }


    rotations:
    // rb.AddTorque(transform.up * Input.GetAxis("Horizontal")*rotSpeed - transform.right * Input.GetAxis("Vertical")*rotSpeed);

        /*
        float xRot = Input.GetAxis("Vertical")*rotSpeed;
        float yRot = Input.GetAxis("Horizontal")*rotSpeed;
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);*/

//new Vector3(xRot, yRot, 0);
//transform.rotation=(Vector3.up * Input.GetAxis("Vertical") * rotSpeed + Vector3.right * Input.GetAxis("Horizontal") * rotSpeed);
// transform.rotation = Quaternion.Euler(Input.GetAxis("Vertical") * rotSpeed, Input.GetAxis("Horizontal") * rotSpeed, 0);

/* Vector3 rotation = (Vector3.up * Input.GetAxis("Vertical") + Vector3.right*Input.GetAxis("Horizontal"));
Quaternion deltaRot = Quaternion.Euler(rotation * Time.deltaTime);
rb.MoveRotation(rb.rotation * deltaRot);*/

/*
if (Input.GetAxis("Horizontal") != 0)
{
rb.AddTorque(transform.up * Input.GetAxis("Horizontal") * rotSpeed, ForceMode.VelocityChange);
}
if (Input.GetAxis("Horizontal") == 0)
{
rb.AddTorque(transform.up  * rotSpeed, ForceMode.VelocityChange);
}*/

//Debug.Log("vertical: "+Input.GetAxis("Vertical"));
//Debug.Log("horizontal: "+Input.GetAxis("Horizontal"));
//Rotate(transform);
/*


currentVel = new Vector3(cc.velocity.x, cc.velocity.y, cc.velocity.z);
vel = currentVel.sqrMagnitude;
Debug.Log("currentVelo: " + vel);
//accelerate with x until certain speed limit is reached
if (Input.GetButton("move") && vel < maxSpeed)
{
    moveDirection = transform.forward;
    moveDirection *= moveSpeed;
}
//stop with triangle(gradually)
if (Input.GetButton("stop")&&vel>0)
{
    moveDirection += -transform.forward;
    moveDirection *= stopSpeed;
}

cc.Move(moveDirection);*/


/*moveDirection = -transform.forward;
moveDirection *= stopSpeed ;
cc.Move(moveDirection);*/
/*Vector3 stop = -transform.forward * stopSpeed;
rb.AddForce(stop);*/
/* Vector3 force = transform.forward * moveSpeed;
 rb.AddForce(force);
 ClampVelocity();*/
/* Debug.Log("rotating");
moveDirection = transform.forward;
moveDirection *= moveSpeed ;
cc.Move(moveDirection);*/


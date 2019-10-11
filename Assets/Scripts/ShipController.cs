using System.Collections;
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


    private GameObject engineSlot;
    //engine specs
    private float moveSpeed;
    private float stopSpeed;
    private float maxRotSpeed;
    private float minRotSpeed;
    private float maxSpeed;
    private float maxTilt;

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


       component = (float)Math.Pow(minRotSpeed / maxRotSpeed, 1 / maxSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Rotate();
        //moves ship forward until while clamping the velocity (hindering inifinte velocity)
        if (Input.GetButton("move")&&forwardVel<maxSpeed)
        {
            forwardVel += moveSpeed;
        }
        //stops ship without turning it around
        if (Input.GetButton("stop")&&Vector3.Dot(transform.forward,rb.velocity)>0)
        {

            forwardVel -= stopSpeed;
        }

        //clamp the velocity for the display
        forwardVel = Mathf.Clamp(forwardVel, 0, maxSpeed);
        //actually moves the ship
        rb.velocity = transform.forward * forwardVel;
        //display current velocity
        velocityText.text = forwardVel.ToString("0.0"); 
    }

    
    private void Rotate()
    {
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


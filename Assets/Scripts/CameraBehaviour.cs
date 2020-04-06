using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject target;
    public float smoothMin;
    private float actualSmooth;
    public float rotSmooth;
    public Transform moveTo;
    private float velocity;
    private float maxVel;

    void Start()
    {
        maxVel = target.GetComponent<ShipController>().getMaxVel();

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        velocity = target.GetComponent<ShipController>().getForwardVel();


        actualSmooth = ((smoothMin-1) / maxVel)*velocity+1;
        //transform.position = Vector3.SmoothDamp(transform.position,moveTo.position, ref velocity, smooth);
        transform.position = Vector3.Lerp(transform.position,moveTo.position, actualSmooth);

        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, rotSmooth );
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, rotSmooth );
        // transform.rotation = Vector3.Lerp(transform.rotation,moveTo,smooth);
    }
}

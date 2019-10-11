using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Transform rotateAround;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotateAround.position, Vector3.up, 20 * Time.deltaTime);
    }
}

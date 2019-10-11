using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward*0.5f;
    }
}

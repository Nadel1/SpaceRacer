using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSpecs : MonoBehaviour
{
    public float moveSpeed;
    public float stopSpeed;
    public float maxRotSpeed;
    public float minRotSpeed;
    public float maxSpeed;
    public float maxTilt;

    public float getMoveSpeed()
    {
        return moveSpeed;
    }
    public float getStopSpeed()
    {
        return stopSpeed;
    }
    public float getMaxRotSpeed()
    {
        return maxRotSpeed;
    }
    public float getMinRotSpeed()
    {
        return minRotSpeed;
    }
    public float getMaxSpeed()
    {
        return maxSpeed;
    }

    public float getMaxTilt()
    {
        return maxTilt;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 cameraInitialPosition;
    public float maxShakeMagnitude = 1f, shakeTime = 0.5f;
    private float shakeMagnitude;
    public float maxDistance;
    private float distance; 

    public void ShakeIt(float distance)
    {
        this.distance = distance;
        //the closer the camera to the explosion, the more it gets shaken
        shakeMagnitude = (-maxShakeMagnitude / maxDistance) * distance + maxShakeMagnitude;
        cameraInitialPosition = this.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePos = this.transform.position;
        cameraIntermediatePos.x += cameraShakingOffsetX;
        cameraIntermediatePos.y += cameraShakingOffsetY;
        this.transform.position = cameraIntermediatePos;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        this.transform.position = cameraInitialPosition;
    }
}

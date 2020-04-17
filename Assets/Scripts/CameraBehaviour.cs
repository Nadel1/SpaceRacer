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
    //shake frequency, larger value means faster shake
    public float frequency = 25;
    //values to enhance rumble along certain axis
    public Vector3 maximumTranslationShake = Vector3.one * 0.5f;
    public Vector3 maximumAngularShake = Vector3.one * 2;
    //time the rumble needs to go back to initial state
    public float recoverySpeed = 1.5f;
    //finer exponent to fade it out nicer
    public float traumaExponent = 2;

    //induced stress value, used as a multiplier
    private float trauma = 0;

    //for random perlin noise values
    private float seed;
    private float shake;
    // Start is called before the first frame update
    private void Awake()
    {
        seed = Random.value;
    }

    void Start()
    {
        maxVel = target.GetComponent<ShipController>().getMaxVel();

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        velocity = target.GetComponent<ShipController>().getForwardVel();


        actualSmooth = ((smoothMin-1) / maxVel)*velocity+1;
        Debug.Log( maxVel);
        //transform.position = Vector3.SmoothDamp(transform.position,moveTo.position, ref velocity, smooth);
        transform.position = Vector3.Lerp(transform.position,moveTo.position, actualSmooth);

        shake = Mathf.Pow(trauma, traumaExponent);

        //calculation of the position rumble using perlin noise
        transform.localPosition += new Vector3(
            maximumTranslationShake.x * Mathf.PerlinNoise(seed, Time.time * frequency)  -0.5f,
            maximumTranslationShake.y * Mathf.PerlinNoise(seed + 1, Time.time * frequency) - 0.5f,
            maximumTranslationShake.z * Mathf.PerlinNoise(seed + 2, Time.time * frequency) - 0.5f
        ) * shake;
        
       
        trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, rotSmooth );
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, rotSmooth );
        // transform.rotation = Vector3.Lerp(transform.rotation,moveTo,smooth);
    }

    public void induceStress(float stress)
    {
        trauma = Mathf.Clamp01(trauma + stress);
    }
}

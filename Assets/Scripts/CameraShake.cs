using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        shake = Mathf.Pow(trauma, traumaExponent);

        //calculation of the position rumble using perlin noise
        transform.localPosition = new Vector3(
            maximumTranslationShake.x * Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1,
            maximumTranslationShake.y * Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1,
            maximumTranslationShake.z * Mathf.PerlinNoise(seed + 2, Time.time * frequency) * 2 - 1
        ) * shake;
        
        //reduces trauma over time
        trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
    }

    //called by the enemies to start the rumble
    public void induceStress(float stress)
    {
        trauma = Mathf.Clamp01(trauma + stress);
    }
}

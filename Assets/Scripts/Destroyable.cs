using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float hitPoints;
    private float fullHealth;
    public float damage;
    private Rigidbody rb;
    public ParticleSystem expl;
    //use gameController to shake camera
    private GameObject gameController;

    //shake variables
    private Vector3 initialPosition;
    public float maxShakeMagnitude = 1f, shakeTime = 0.5f;
    private float shakeMagnitude;

    //spawn variables
    public float spawnMin;
    public float spawnMax;
    private GameObject[] allRes;
    public float offset = 30;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Random.rotation;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * Random.Range(0.5f, 3f), ForceMode.Impulse);
        gameController = GameObject.Find("GameController");
        fullHealth = hitPoints;
        //get all available ressources 
        allRes = GameObject.Find("GameController").GetComponent<Ressources>().ressources;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            float remove = col.gameObject.GetComponent<ProjectileSpecs>().getDamage();
            hitPoints -= remove;
            Destroy(col.gameObject);
            ShakeIt();
            if (hitPoints <= 0)
            {
                //instantiate explosion particle system
                Instantiate(expl, transform.position, Quaternion.identity);
                //make camera shake
                gameController.GetComponent<DetectExplosion>().setExplosion(true);
                gameController.GetComponent<DetectExplosion>().setExplosionOrigin(transform.position);
                //spawn ressources
                spawnRes();
                Destroy(this.gameObject);
            }
        }
        
        
    }
    void spawnRes()
    {
        float resSize = Random.Range(spawnMin, spawnMax);

        for(int i=0; i< resSize; i++)
        {
            int index = Random.Range(0, allRes.Length);
            Vector3 spawnPos;
            spawnPos.x = Random.Range(-offset, offset);
            spawnPos.y = Random.Range(-offset, offset);
            spawnPos.z = Random.Range(-offset, offset);
            Instantiate(allRes[index], transform.position+spawnPos, Quaternion.identity);
        }
    }

    void ShakeIt()
    {
        //the less health the meteor has the more it gets shaken
        shakeMagnitude = (-maxShakeMagnitude / fullHealth) * hitPoints + maxShakeMagnitude;
        initialPosition = this.transform.position;
        InvokeRepeating("StartShaking", 0f, 0.005f);
        Invoke("StopShaking", shakeTime);
    }

    void StartShaking()
    {
        //compute random offsets and apply them
        float shakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float shakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 IntermediatePos = this.transform.position;
        IntermediatePos.x += shakingOffsetX;
        IntermediatePos.y += shakingOffsetY;
        this.transform.position = IntermediatePos;
    }

    void StopShaking()
    {
        CancelInvoke("StartShaking");
        this.transform.position = initialPosition;
    }
    public float getDamage()
    {
        return damage;
    }
}

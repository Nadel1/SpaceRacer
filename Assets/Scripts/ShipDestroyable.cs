using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestroyable : MonoBehaviour
{
    public float hitPoints;
    private Rigidbody rb;
   
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            float remove = col.gameObject.GetComponent<ProjectileSpecs>().getDamage();
            hitPoints -= remove;
            Destroy(col.gameObject);
            if (hitPoints <= 0)
            {
                //Destroy(this.gameObject);
            }
        }
        if (col.gameObject.tag == "Comet")
        {
            float maxSpeed = GetComponentInParent<ShipController>().getMaxVel();
            float currentSpeed = GetComponentInParent<ShipController>().getForwardVel();

            float damage = (currentSpeed / maxSpeed) * col.gameObject.GetComponent<Destroyable>().getDamage();
        }
        

    }
    // Update is called once per frame
    void Update()
    {

    }
}

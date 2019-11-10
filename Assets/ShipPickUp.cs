using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPickUp : MonoBehaviour
{
    private int[] inventory;

    void Start()
    {
        if (GetComponent<ShipController>().number == 1)
        {
            inventory = GameObject.FindGameObjectWithTag("GameController1").GetComponent<Ressources>().inv;
        }
        else
        {
            inventory = GameObject.FindGameObjectWithTag("GameController2").GetComponent<Ressources>().inv;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            int index = other.gameObject.GetComponent<PickUpSpecs>().index;
            inventory[index] += 1;
            Destroy(other.gameObject);
        }
    }
}

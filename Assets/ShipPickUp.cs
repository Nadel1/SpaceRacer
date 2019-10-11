using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPickUp : MonoBehaviour
{
    private int[] inventory;

    void Start()
    {
        inventory = GameObject.Find("GameController").GetComponent<Ressources>().inv;
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

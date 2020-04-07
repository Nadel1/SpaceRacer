using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefuelShip : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;
    public TextMeshProUGUI serviceText;
    public Transform parkingSpace;
    private GameObject ship;

    private void Start()
    {
        welcomeText.text = "Welcome to the Gasstation!";
        serviceText.text = "What can we do for you?\n Refuel: right arrow";
        welcomeText.gameObject.SetActive(false);
        serviceText.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (welcomeText.IsActive())
        {
           ship.transform.position = Vector3.Lerp(ship.transform.position,parkingSpace.transform.position, 2f*Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerShip")
        {
            welcomeText.gameObject.SetActive(true);
            serviceText.gameObject.SetActive(true);
            //check wether or not parking space is full
            //position the ship in parking space

            ship = other.gameObject;
            //block ships movement
            other.gameObject.GetComponent<ShipController>().block();
            //other.gameObject.transform.position = Vector3.MoveTowards(transform.position, parkingSpace.position, 0.5f);
            //todo: fix the rotation so that the ship faces along the parking space
            //other.gameObject.transform.Rotate(0, 90, 0, Space.World);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerShip")
        {
            welcomeText.gameObject.SetActive(false);
            serviceText.gameObject.SetActive(false);
        }
    }
   
}

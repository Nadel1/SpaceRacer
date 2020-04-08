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
        serviceText.text = "What can we do for you?\n Refuel: O\n Exit: square";
        welcomeText.gameObject.SetActive(false);
        serviceText.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (welcomeText.IsActive())
        {
            //position the ship in parking space smoothly
            ship.transform.position = Vector3.Lerp(ship.transform.position,parkingSpace.transform.position, 2f*Time.fixedDeltaTime);
            ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation,parkingSpace.transform.rotation,2*Time.fixedDeltaTime);
            if (Input.GetButton("Confirm"))
            {
                //set current filled of the ship to its max and give the player the controll over the ship back
                ship.GetComponent<ShipController>().setCurrentFilled(ship.GetComponent<ShipController>().getMaxCap());
                serviceText.text = "Alright, all done!\n Have a nice day";
                ship.GetComponent<ShipController>().SetState(ShipController.States.Free);
            }
            //unblock the ships movement when player cancels
            if (Input.GetButton("Cancel"))
            {
                ship.GetComponent<ShipController>().SetState(ShipController.States.Free);
            }
               
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerShip")
        {
            welcomeText.gameObject.SetActive(true);
            serviceText.gameObject.SetActive(true);
            //check wether or not parking space is full
            ship = other.gameObject;
            //block ships movement
            other.gameObject.GetComponent<ShipController>().block();
            //todo: fix the rotation so that the ship faces along the parking space
            
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

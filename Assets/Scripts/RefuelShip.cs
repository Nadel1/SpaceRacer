using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefuelShip : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;
    public TextMeshProUGUI serviceText;
    public string service="What can we do for you?\n Refuel: O\n Exit: square";

    public Transform parkingSpace;
    private GameObject ship;
    //pricing system
    public float minPricePerLiter=5;
    public float priceTime=30;
    private float availabe;
    public float maxCapacityFuel= 10000;
    public float averageChange=4;

    //price is public so that change can be observed in the inspector on runtime
    public float price=0;
    private float literDifference;
    private float extra;

    //check if coroutine to hold a price is still running
    private bool isStable;
    private void Start()
    {
        welcomeText.text = "Welcome to the Gasstation!";

        service="What can we do for you?\n Refuel: O\n Exit: square";
        welcomeText.gameObject.SetActive(false);
        serviceText.gameObject.SetActive(false);
        extra = 0;
        isStable = false;
        availabe = maxCapacityFuel;
    }
    private void FixedUpdate()
    {
        if (welcomeText.IsActive())
        {
            //position and rotates the ship in parking space smoothly
            ship.transform.position = Vector3.Slerp(ship.transform.position,parkingSpace.transform.position, 3f*Time.fixedDeltaTime);
            ship.transform.rotation = Quaternion.Slerp(ship.transform.rotation,parkingSpace.transform.rotation,3*Time.fixedDeltaTime);
            if (Input.GetButton("Confirm"))
            {
                Refuel();
                serviceText.text = "Price: " +price.ToString("0.0") + "\nAlright, all done!\n Have a nice day";
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
            serviceText.text = service;
            welcomeText.gameObject.SetActive(true);
            serviceText.gameObject.SetActive(true);
            //check wether or not parking space is full
            ship = other.gameObject;
            //block ships movement
            other.gameObject.GetComponent<ShipController>().block();
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

    private void Refuel()
    {
        //calculate liter difference
        literDifference = ship.GetComponent<ShipController>().getMaxCap() - ship.GetComponent<ShipController>().getCurrentFilled();
        price = literDifference * minPricePerLiter+CalculateExtraCost();
        availabe -= literDifference;
        //set current filled of the ship to its max 
        ship.GetComponent<ShipController>().setCurrentFilled(ship.GetComponent<ShipController>().getMaxCap());
    }

    private float CalculateExtraCost()
    {
        //price goes up the less is available
        if (isStable)
        {
            return extra;
        }
        else
        {
            extra = Random.Range(averageChange - 2, averageChange + 2) * maxCapacityFuel / availabe;
            StartCoroutine(HoldPrice());
            return extra;
        }
        
    }

    IEnumerator HoldPrice()
    {
        isStable = true;
        yield return new WaitForSeconds(priceTime);
        isStable = false;
    }
}

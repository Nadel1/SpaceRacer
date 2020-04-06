using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefuelShip : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;
    public TextMeshProUGUI serviceText;

    private void Start()
    {
        welcomeText.text = "Welcome to the Gasstation!";
        serviceText.text = "What can we do for you?\n Refuel: right arrow";
        welcomeText.gameObject.SetActive(false);
        serviceText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (welcomeText.IsActive())
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerShip")
        {
            welcomeText.gameObject.SetActive(true);
            serviceText.gameObject.SetActive(true);
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

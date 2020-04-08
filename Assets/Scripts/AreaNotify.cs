using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaNotify : MonoBehaviour
{
    public TextMeshProUGUI areaText;
    public string text;
    public float waitForSeconds;
    // Start is called before the first frame update
    void Start()
    {
       
        areaText.text = text;
        areaText.gameObject.SetActive(false);
        
    }

    IEnumerator Display()
    {
        areaText.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitForSeconds);
        areaText.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "PlayerShip")
        {
            //show the notifier for a certain amount of time, afterwards the notification disappears
            StartCoroutine(Display());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerShip")
        {
            //the notification should disappear once the player leaves the area
            areaText.gameObject.SetActive(false);
        }
    }
}

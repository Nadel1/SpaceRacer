using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaNotify : MonoBehaviour
{
    public TextMeshProUGUI areaText;
    public string text;
    // Start is called before the first frame update
    void Start()
    {
       
        areaText.text = text;
        areaText.gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "PlayerShip")
        {
            areaText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerShip")
        {
            areaText.gameObject.SetActive(false);
        }
    }
}

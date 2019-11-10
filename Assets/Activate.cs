using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Activate : MonoBehaviour
{
    //enables the menu function through controller input
    private GameObject nextSelected;
    private GameObject par;
    public EventSystem ev;
    private string lastActive;

    //needed to display current text
    private GameObject currentSelected;
    private GameObject textField;
    private GameObject statsField;
    private string description;
    private string stats;


    void Start()
    {
        lastActive = "";
        textField = GameObject.FindWithTag("TextField");
    }
    // Update is called once per frame
    void Update()
    {


        EnableMenu();
        ShowText();
        
            
    }

    //sets the first selected correctly for each new menu
    void EnableMenu()
    {
        nextSelected = GameObject.FindWithTag("selected");
        //check wether or not there has been something selected or the menu that opened is new
        if (lastActive.Equals("") || nextSelected.transform.parent.name != lastActive)
        {
            ev.GetComponent<EventSystem>().SetSelectedGameObject(nextSelected, new BaseEventData(ev));
            lastActive = nextSelected.transform.parent.name;
        }
    }

    //displays the information
    void ShowText()
    {
        textField = GameObject.FindWithTag("TextField");
        statsField = GameObject.FindWithTag("StatsField");
        currentSelected = ev.currentSelectedGameObject;

        //get the information
        description = currentSelected.GetComponent<Description>().description;
        stats = currentSelected.GetComponent<CraftingStats>().stats;

        //display it
        textField.GetComponent<TMPro.TextMeshProUGUI>().text=description;
        statsField.GetComponent<TMPro.TextMeshProUGUI>().text = stats;

    }
}

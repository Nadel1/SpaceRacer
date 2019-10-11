using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBuildable : MonoBehaviour
{
    public GameObject build;
    //full inventory
    private int[] inventory;
    //information hold by the prefab
    private GameObject[] neededMat;
    private int[] neededQuant;
    // Start is called before the first frame update
    void Start()
    {
        neededMat = build.GetComponent<CraftingSpecs>().ressources;
        neededQuant= build.GetComponent<CraftingSpecs>().quantity;
        inventory = GameObject.Find("GameController").GetComponent<Ressources>().inv;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().interactable = CheckResources();
    }

    bool CheckResources()
    {
        for(int i = 0; i < neededMat.Length; i++)
        {
            int checkAt = neededMat[i].GetComponent<PickUpSpecs>().index;
            if (inventory[checkAt] < neededQuant[i])
            {
                //breaks when at least one quantity is missing
                return false;
            }
        }
        return true;
    }
}

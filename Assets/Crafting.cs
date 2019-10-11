using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    //ship variables 
    public Transform engineSlot;
    public Transform gunSlot;

    private Transform remove;
      
    public void BuildEngine(GameObject build)
    {
        //find the current engine
        remove = engineSlot.GetChild(0);
        //instantiate new engine as child of engineSlot
        Instantiate(build, engineSlot);
        build.transform.localPosition = new Vector3(0, 0.25f, 0);
        Destroy(remove.gameObject);
    }

    public void BuildGun()
    {

    }
}

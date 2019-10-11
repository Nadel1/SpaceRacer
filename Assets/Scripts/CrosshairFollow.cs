using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairFollow : MonoBehaviour
{
    public Transform follow;
    private RectTransform rt;
    private Camera cam;
    
    void Start()
    {
        rt = GetComponent<RectTransform>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.position = cam.WorldToScreenPoint(follow.position);
    }
}

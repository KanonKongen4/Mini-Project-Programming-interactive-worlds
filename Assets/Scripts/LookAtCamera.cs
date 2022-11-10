using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class LookAtCamera : MonoBehaviour
{
    public Transform cam;
    bool lookAtMain;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MiniMapCam").transform;
        if(lookAtMain) cam = Camera.main.transform;
    }


    void Update()
    {
        transform.LookAt(cam);
    }
}

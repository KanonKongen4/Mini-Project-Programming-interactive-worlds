using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class SceneDressing : MonoBehaviour
{
    
    void Start()
    {
        for(int i = 0; i < 100; i++)
        {

           GameObject Cloud = Instantiate(Resources.Load("Cloud"), Random.insideUnitSphere*400, Quaternion.identity) as GameObject;
            Cloud.transform.localScale += Random.insideUnitSphere * 50;
        }
    }


    void Update()
    {
        
    }


}

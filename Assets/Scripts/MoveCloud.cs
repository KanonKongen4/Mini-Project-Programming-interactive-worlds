using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class MoveCloud : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward* Random.Range(10,30));
    }


    void Update()
    {
        
    }
}

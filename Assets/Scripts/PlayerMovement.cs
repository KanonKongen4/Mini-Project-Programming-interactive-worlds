using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class PlayerMovement : MonoBehaviour
{
    private AnimatePlane animatePlane;
    private Rigidbody rb;
    bool accelerate;
    public float speed = 0, rotationSpeed = 100f, autoAimRotaionSpeed = 0.004f;
    float speedMax = 10f;
    float accelerationSpeed = 0.2f;
    private FollowCamera followCamera;
    
    
    void Start()
    {

        followCamera = FindObjectOfType<FollowCamera>();
        rb = GetComponent<Rigidbody>();
        animatePlane = GetComponent<AnimatePlane>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else Time.timeScale = 0;
        }

        speed = speedMax;
        if (Input.GetKey(KeyCode.LeftShift))
            speed = speedMax * 7;

         transform.position += transform.forward * (speed * Time.deltaTime);
        
        
            transform.Rotate(Input.GetAxis("Vertical") * (rotationSpeed * Time.deltaTime), 0, -Input.GetAxis("Horizontal") * (rotationSpeed * Time.deltaTime));
            if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) // Makes the plane rotate towards the camera rotation, thus speeding up the aligment
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, followCamera.transform.rotation, 6f*Time.deltaTime);
        }

      
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animatePlane.SetElevatorRotation(true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animatePlane.SetElevatorRotation(false);
        }

        animatePlane.SetPropellerSpeed(speed);

    }

}
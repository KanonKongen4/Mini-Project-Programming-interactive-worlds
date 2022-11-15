using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for: Animating the player plane
public class AnimatePlane : MonoBehaviour
{
    public Transform elevator, aileron_left, aileron_right, propeller;
    private float propellerSpeed;

  
    void Update()
    {
        propeller.Rotate(0, propellerSpeed, 0, Space.Self);
    }

    public void SetElevatorRotation(bool bol)
    {
       // elevator.rotation = elevatorStartRotation;
        if (bol)
            elevator.Rotate(0, 45, 0, Space.Self);
        else
            elevator.Rotate(0, 45, 0, Space.Self);
    }
    public void SetAileronLeftRotation(bool bol)
    {
        if (bol)
            aileron_left.rotation = Quaternion.Euler(0, 45, 0);
        else
            aileron_left.rotation = Quaternion.Euler(0, -45, 0);
    }
    public void SetAileronRightRotation(bool bol)
    {
        if (bol)
            aileron_right.rotation = Quaternion.Euler(0, 45, 0);
        else
            aileron_right.rotation = Quaternion.Euler(0, -45, 0);
    }
    public void SetPropellerSpeed(float speed)
    {
        propellerSpeed = speed;
    }
}
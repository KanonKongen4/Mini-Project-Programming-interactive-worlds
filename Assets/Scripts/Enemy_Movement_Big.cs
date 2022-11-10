using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class Enemy_Movement_Big : MonoBehaviour
{
    public enum MovementType { Attacking, Fleeing }
    public MovementType movementType = MovementType.Attacking;
    public Transform movementMentLooker, player;
    private IEnumerator changeDirection, playerLooker;
    private float speed = 0, rotationSpeed = 0.5f;
    float speedMax = 10f, rotationSpeedMax = 1f;
    float accelerationSpeed = 0.2f;

    Quaternion heading = Quaternion.identity;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        changeDirection = DirectionChanger();
        playerLooker = LookAtPlayerAtIntervals();
        StartCoroutine(playerLooker);
        StartCoroutine(StateChanger());
    }


    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, heading, rotationSpeed * Time.deltaTime);
    }

    private void ChangeStates()
    {
        if (movementType == MovementType.Attacking)
        {
            movementType = MovementType.Fleeing;
            StopCoroutine(playerLooker);
            StartCoroutine(changeDirection);
        }
        else
        {
            movementType = MovementType.Attacking;
            StartCoroutine(playerLooker);
            StopCoroutine(changeDirection);
        }
    }

    IEnumerator StateChanger()
    {
        while (true)
        {
            ChangeStates();
            yield return new WaitForSeconds(11);
        }
    }

    IEnumerator DirectionChanger()  
    {
        rotationSpeed = rotationSpeedMax;
        speed = speedMax * 1;
        while (true)
        {
            heading = Quaternion.Euler(0, Random.Range(0,360), 0);
          

            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator LookAtPlayerAtIntervals()
    {
        rotationSpeed = rotationSpeedMax * 4;
        speed = speedMax;
        while (true)
        {

            movementMentLooker.LookAt(new Vector3(player.position.x,0,player.position.z));
            
            heading = movementMentLooker.rotation;
            yield return new WaitForSeconds(1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for: Smoothly following the player and shaking the screen
public class FollowCamera : MonoBehaviour
{

    private IEnumerator screenShaker;
    private Vector3 newPos;
    private float distToPlayer, followDistanceOriginal;
    public Transform camFollowPoint;
    public float followSpeed = 0.3f, rotateSpeed = 0.4f;
    public float followDistance = 10f, followHeight = 5f;
    float mg = 2;

    void Start()
    {

        screenShaker = ShakeScreenContinously();
        camFollowPoint = GameObject.FindGameObjectWithTag("CamFollow").transform;
        followDistanceOriginal = followDistance;
    }

    void Update()
    {

        newPos = (camFollowPoint.position - camFollowPoint.forward * followDistance) + (camFollowPoint.up * followHeight); //Calculating the new target position for the camera
       
        transform.position = Vector3.Lerp(transform.position,newPos,followSpeed * Time.deltaTime); // Lerping towards the position
        transform.rotation = Quaternion.Lerp(transform.rotation, camFollowPoint.rotation, rotateSpeed * Time.deltaTime);//lerping towards the rotation of the follow object


    }

    public void ShakeScreen()
    {
        StartCoroutine(screenShaker);
        mg = 2;
        Invoke(nameof(StopScreenShake), 1f);
    }

   private void StopScreenShake()
    {
        StopCoroutine(screenShaker);

    }
    IEnumerator ShakeScreenContinously()
    {
       
        while (true)
        {
            mg -= 0.4f;  //Reducing the magnitude of the shake over time
            if(mg < 0) mg = 0; // Avoiding the magnitude becoming negative, thus beginnning to shake again
            transform.position += new Vector3(Random.Range(-mg, mg), Random.Range(-mg, mg), 0); //moving the camera in random ways 
            yield return new WaitForSeconds(0.05f);
        }
    }
}

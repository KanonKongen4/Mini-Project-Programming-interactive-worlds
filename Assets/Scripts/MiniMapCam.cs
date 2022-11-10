using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class MiniMapCam : MonoBehaviour
{
    public Transform camFollowPoint;
    private Vector3 newPos;

    private float followSpeed = 5f, rotateSpeed = 6f;
    private float followDistance = 155, followHeight = 45f;
    void Start()
    {
        camFollowPoint = GameObject.FindGameObjectWithTag("CamFollow").transform;
    }


    void Update()
    {
        //distToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        newPos = (camFollowPoint.position - camFollowPoint.forward * followDistance) + (camFollowPoint.up * followHeight);

        transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, camFollowPoint.rotation, rotateSpeed * Time.deltaTime);
    }
}

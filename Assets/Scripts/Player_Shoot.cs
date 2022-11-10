using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class Player_Shoot : MonoBehaviour
{
    private FindMostCenterObject findMostCenterObject;

    private IEnumerator shootCoroutine;
    private float interval = 0.07f;
    public Transform eye, gunRotation;
    public Transform gunRight, gunLeft;
    private SoundPlayerPool soundPool;
    void Start()
    {
        soundPool = FindObjectOfType<SoundPlayerPool>();
        findMostCenterObject = FindObjectOfType<FindMostCenterObject>();
                shootCoroutine = Shoot();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(shootCoroutine);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(shootCoroutine);
        }

        if(findMostCenterObject.targetObject != null)
        {
            eye.LookAt(findMostCenterObject.targetObject.transform.position);
            gunRotation.transform.rotation = Quaternion.Slerp(transform.rotation, eye.transform.rotation,50f* Time.deltaTime);
        }
        else gunRotation.transform.forward = transform.forward;
    }
    public void ResetGunRotation()
    {
        gunRotation.transform.forward = transform.forward;
    }
    private IEnumerator Shoot()
    {
        while (true)
        {
            RaycastHit hit;

            Vector3 p1 = transform.position + transform.forward * 1.2f;
            float distanceToObstacle = 0;

            GameObject Bullet = Instantiate(Resources.Load("Bullet"), gunLeft.position + transform.forward * 7, gunRotation.rotation) as GameObject;
            Bullet.GetComponent<Rigidbody>().velocity = gunRotation.forward * 800;
            Destroy(Bullet, 1);
            GameObject FX_ShootEnemy = Instantiate(Resources.Load("FX_ShootPlayer"), gunLeft.position + transform.forward * 7, gunRotation.rotation) as GameObject;
            Destroy(FX_ShootEnemy, 10);

            GameObject Bullet2 = Instantiate(Resources.Load("Bullet"), gunRight.position + transform.forward * 7, gunRotation.rotation) as GameObject;
            Bullet2.GetComponent<Rigidbody>().velocity = gunRotation.forward * 800;
            Destroy(Bullet2, 1);
            GameObject FX_ShootEnemyRight = Instantiate(Resources.Load("FX_ShootPlayer"), gunRight.position + transform.forward * 7, gunRotation.rotation) as GameObject;
            Destroy(FX_ShootEnemyRight, 10);
            // Cast a sphere wrapping character controller 10 meters forward
            // to see if it is about to hit anything.

            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 3;

            // Does the ray intersect any objects excluding the player layer

            if (Physics.SphereCast(p1, 9f, gunRotation.forward, out hit, 260, layerMask))
            {
                if (hit.transform.gameObject.GetComponent<Enemy_Health>())
                {
                    hit.transform.gameObject.GetComponent<Enemy_Health>().TakeDamage(4);
                    GameObject FX_BulletHit = Instantiate(Resources.Load("FX_BulletHit_Hit"), hit.point, Quaternion.identity) as GameObject;
                    Destroy(FX_BulletHit, 5);
                }
                else
                {
                    GameObject FX_BulletHit = Instantiate(Resources.Load("FX_BulletHit"), hit.point, Quaternion.identity) as GameObject;
                    Destroy(FX_BulletHit, 5);
                }
                //  distanceToObstacle = hit.distance;
            }
            soundPool.PlaySound(transform.position, soundPool.GetRandomGutSound());

            yield return new WaitForSeconds(interval);
        }
    }
}
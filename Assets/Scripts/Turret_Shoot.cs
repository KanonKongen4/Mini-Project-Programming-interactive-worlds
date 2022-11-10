using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class Turret_Shoot : MonoBehaviour
{
    public Transform gunMouth, player, eye;
    private float rotateSpeed = 4f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        eye.transform.LookAt(player);
        transform.rotation = Quaternion.Lerp(transform.rotation, eye.transform.rotation, rotateSpeed * Time.deltaTime);
    }
    IEnumerator Shoot()
    {

        while (true)
        {
                PullShot();
            yield return new WaitForSeconds(5f);
        }
    }
    private void PullShot()
    {
        GameObject BulletEnemy = Instantiate(Resources.Load("BulletEnemyBig"), gunMouth.position, transform.rotation) as GameObject;
        BulletEnemy.GetComponent<Rigidbody>().velocity = transform.forward * 60;
        Destroy(BulletEnemy, 10);

        GameObject FX_ShootEnemy = Instantiate(Resources.Load("FX_ShootEnemy_Big"), gunMouth.position, transform.rotation) as GameObject;
        Destroy(FX_ShootEnemy, 10);
    }
}

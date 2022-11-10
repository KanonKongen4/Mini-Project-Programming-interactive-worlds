using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class Enemy_Shoot : MonoBehaviour
{
    private SoundPlayerPool soundPlayerPool;

    private Enemy_Movement enemy_Movement;
    public Transform gunLeft, gunRight;
    void Start()
    {
        soundPlayerPool = FindObjectOfType<SoundPlayerPool>();

        enemy_Movement = GetComponent<Enemy_Movement>();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (enemy_Movement.movementType == Enemy_Movement.MovementType.Attacking)
            {
                PullShot();
            }
            yield return new WaitForSeconds(0.23f);
        }
    }
    private void PullShot()
    {
        soundPlayerPool.PlaySound(transform.position, soundPlayerPool.enemyShoot);
        GameObject BulletEnemy = Instantiate(Resources.Load("BulletEnemy"), transform.position + transform.forward * 4, transform.rotation) as GameObject;
        BulletEnemy.GetComponent<Rigidbody>().velocity = transform.forward * 60;
        Destroy(BulletEnemy, 10);

              GameObject FX_ShootEnemy = Instantiate(Resources.Load("FX_ShootEnemy"), transform.position + transform.forward * 6, transform.rotation) as GameObject;
        Destroy(FX_ShootEnemy, 2);
    }
}
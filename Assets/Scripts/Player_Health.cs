using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//:) This script is responsible for:
public class Player_Health : MonoBehaviour
{
    private SoundPlayerPool soundPlayerPool;

    private GameController gameController;
    private float maxHp = 20, hp;
    public FollowCamera followCamera;
    public Image hpImage;
    private bool dead;
    void Start()
    {
        soundPlayerPool = FindObjectOfType<SoundPlayerPool>();

        gameController = FindObjectOfType<GameController>();
        followCamera = FindObjectOfType<FollowCamera>();
        hpImage = GameObject.Find("HPSlider").GetComponent<Image>();
        hp = maxHp;
        hpImage.fillAmount = hp / maxHp;
    }
    public void Heal()
    {
        hp += 10;
        if(hp > maxHp) hp = maxHp;

        hpImage.fillAmount = hp / maxHp;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet" && !dead)
        {
            Destroy(other.gameObject);
            TakeDamage(2);
            GameObject FX_BulletHit = Instantiate(Resources.Load("FX_BulletHit_Hit"), other.transform.position, Quaternion.identity) as GameObject;
            Destroy(FX_BulletHit, 5);
            followCamera.ShakeScreen();
        }
        if (other.gameObject.tag == "EnemyBulletBig" && !dead)
        {
            Destroy(other.gameObject);
                        TakeDamage(6);
            GameObject FX_BulletHit = Instantiate(Resources.Load("FX_BulletHit_Hit"), other.transform.position, Quaternion.identity) as GameObject;
            Destroy(FX_BulletHit, 5);
            followCamera.ShakeScreen();
        }
    }
    public void TakeDamage(int dmg)
    {
        soundPlayerPool.PlaySound(transform.position, soundPlayerPool.playerHit);
        hp -= dmg;
        hpImage.fillAmount = hp / maxHp;
        if (hp <= 0 && !dead) Die();

    }
    private void Die()
    {dead = true;
        GameObject FX_Explosion = Instantiate(Resources.Load("FX_Explosion"), transform.position, Quaternion.identity) as GameObject;
        Destroy(FX_Explosion, 15);
        followCamera.ShakeScreen();

        gameController.DisplayDeadText();

        Destroy(GetComponent<PlayerMovement>());
        Destroy(GetComponent<Player_Shoot>());
        gameObject.AddComponent<Rigidbody>();


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class Enemy_Health : MonoBehaviour, Destructable
{
    private SoundPlayerPool soundPlayerPool;

    private GameController gameController;
    private SpawnEnemies spawnEnemies;
    public int maxHp = 10, hp;
    public List<Rigidbody> bits;
    public List<ParticleSystem> particles;
    private FollowCamera followCamera;

    public List<Material> materials;
    private FindMostCenterObject findMostCenterObject;
    void Start()
    {
        soundPlayerPool = FindObjectOfType<SoundPlayerPool>();

        findMostCenterObject = FindObjectOfType<FindMostCenterObject>();
        gameController = FindObjectOfType<GameController>();

        spawnEnemies = FindObjectOfType<SpawnEnemies>();
        followCamera = FindObjectOfType<FollowCamera>();
        hp = maxHp;

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            bits.Add(rb);
        }
        foreach (ParticleSystem parSys in GetComponentsInChildren<ParticleSystem>())
        {
            particles.Add(parSys);
        }
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            materials.Add(rend.material);
        }
    }

    public void TakeDamage(int dmg)
    {
        soundPlayerPool.PlaySound(transform.position, soundPlayerPool.succesfulHit);
        hp -= dmg;
        if (hp <= 0) Explode();
    }

    public void Explode()
    {   
        GameObject FX_Explosion = Instantiate(Resources.Load("FX_Explosion"), transform.position, Quaternion.identity) as GameObject;
        Destroy(FX_Explosion, 15);
        Destroy(gameObject, 20);
        followCamera.ShakeScreen();
        MakeIntoBits();
        Destroy(GetComponent<Enemy_Movement>());
        Destroy(GetComponent<Enemy_Shoot>());
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<CapsuleCollider>());
        spawnEnemies.enemies.Remove(gameObject);
        foreach(Material mat in materials)
            mat.SetColor("_BaseColor", new Color(0.3f,0.3f,0.3f));
        Invoke(nameof(ChangeLayer), 0.1f);
        gameController.score++;
    }

    private void MakeIntoBits()
    {
        soundPlayerPool.PlaySound(transform.position, soundPlayerPool.explosion);
        foreach (Rigidbody rb in bits)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(2000, transform.position - Vector3.up * 2f, 13);
            rb.AddTorque(new Vector3(Random.Range(100, 400), 0, Random.Range(100, 600)));
        }

        foreach (ParticleSystem parSys in particles)
        {
            parSys.Play();
        }

        if(gameObject == findMostCenterObject.targetObject)
        {
            findMostCenterObject.targetObject = null;
          findMostCenterObject.MoveCrossOutOfSight();
           // findMostCenterObject.DetachCrosshair();
        }
    }

    private void ChangeLayer()
    {
        gameObject.layer = 0;
        foreach (Rigidbody rb in bits)
        {
            rb.gameObject.layer = 0;
            //if(rb.transform.GetChild(0) != null)
            //rb.transform.GetChild(0).gameObject.layer = 0;
        }
    }
}

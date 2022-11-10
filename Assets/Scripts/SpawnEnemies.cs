using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class SpawnEnemies : MonoBehaviour
{
    private IEnumerator EnemySpawner;
    private MusicPlayer musicPlayer;
    public List<GameObject> enemies = new List<GameObject>();
    private float delay = 9;
    private int enemyLimit = 9;
    void Start()
    {musicPlayer = FindObjectOfType<MusicPlayer>();
        EnemySpawner = SpawnEnemy();

        Invoke(nameof(SpawnFirstWave), 2);
        musicPlayer.Invoke(nameof(musicPlayer.StartMusic),2);

    }
    private void SpawnFirstWave()
    {
        StartCoroutine(EnemySpawner);
        SpawnEnemyAtRandomPos();
        SpawnEnemyAtRandomPos();
        SpawnBigEnemyAtRandomPos();

        StartCoroutine(AddToLimit());
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (enemies.Count < enemyLimit) { 
            int ranNum = Random.Range(0, 5);

                if (ranNum < 4)
                    SpawnEnemyAtRandomPos();
                else
                {
                    SpawnBigEnemyAtRandomPos();
                    SpawnPickUpAtRandomPos();
                }
        }
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnEnemyAtRandomPos()
    {
        Vector3 ranVector = Random.insideUnitSphere * 125;
        GameObject Enemy = Instantiate(Resources.Load("Enemy"), transform.position + ranVector, Quaternion.identity) as GameObject;
        Enemy.name += (Random.Range(0, 1000)).ToString();
        enemies.Add(Enemy);
    }

    private void SpawnPickUpAtRandomPos()
    {
        Vector3 ranVector = Random.insideUnitSphere * 125;
        GameObject PickUp = Instantiate(Resources.Load("PickUp"), transform.position + ranVector, Quaternion.identity) as GameObject;
        PickUp.name += (Random.Range(0, 1000)).ToString();
    }
    private void SpawnBigEnemyAtRandomPos()
    {
        Vector3 ranVector = Random.insideUnitSphere * 125;
        GameObject EnemyBig = Instantiate(Resources.Load("Enemy_Big"), transform.position + ranVector, Quaternion.identity) as GameObject;
        EnemyBig.name += (Random.Range(0, 1000)).ToString();
        enemies.Add(EnemyBig);
    }

    private IEnumerator AddToLimit()
    {
        while (true)
        {
            enemyLimit++;
            delay -= 0.8f;
            if (delay < 2) delay = 2;
            yield return new WaitForSeconds(5);
        }
    }
}

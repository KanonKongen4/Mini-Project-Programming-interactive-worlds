using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class SoundPlayerPool : MonoBehaviour
{
    public AudioClip[] gunSounds;
    public AudioClip explosion;
    public AudioClip pickup, pickupAppear;
    public AudioClip succesfulHit;
    public AudioClip playerHit;
    public AudioClip enemyShoot;

    public List<SoundSource> PooledSources = new List<SoundSource>();

    public Queue<SoundSource> speakerQue = new Queue<SoundSource>();
    void Start()
    {
        AddAourcesToPool(5);
        foreach (SoundSource pooledSound in PooledSources)
        {
            speakerQue.Enqueue(pooledSound);
        }
    }

  
    public void PlaySound(Vector3 pos, AudioClip audioClip)
    {
        if (speakerQue.Count <= 2) AddAourcesToPool(2);
        SoundSource pooledSound = (SoundSource)speakerQue.Dequeue();
        pooledSound.PlaySound(pos, audioClip);

    }

    public AudioClip GetRandomGutSound()
    {
       int ranNum = Random.Range(0, gunSounds.Length);
        return gunSounds[ranNum];
    }
    private void AddAourcesToPool(int num)
    {
        for( int i = 0; i < num; i++)
        {
            GameObject SS = Instantiate(Resources.Load("source")) as GameObject;
            SoundSource soundSource = SS.GetComponent<SoundSource>();
            soundSource.soundPlayerPool = this;
            PooledSources.Add(soundSource);
            speakerQue.Enqueue(soundSource);
        }
    }
}

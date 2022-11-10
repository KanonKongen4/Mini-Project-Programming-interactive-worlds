using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for: Being an object that can be used in a queue
public class SoundSource : MonoBehaviour
{
    public SoundPlayerPool soundPlayerPool;
    public AudioSource audioSource;
 
    public void PlaySound(Vector3 pos, AudioClip clipToPlay)
    {
        if (clipToPlay == null) {
            Debug.Log("fucck is:");

           Debug.Log("clip is:" + clipToPlay + "  name" + clipToPlay.name); }
        audioSource.clip = clipToPlay;
        transform.position = pos;
        audioSource.pitch = 1 + Random.Range(-0.3f, 0.3f);
        audioSource.Play();
        Invoke(nameof(ReturnToQueue), clipToPlay.length+2);
    }
    private void ReturnToQueue()
    {
        audioSource.Stop();
        soundPlayerPool.speakerQue.Enqueue(this);
            }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class MusicPlayer : MonoBehaviour
{
    public AudioClip combat, wind;
    private AudioSource musicSource;
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void RestartMusic()
    {
        musicSource.clip = wind;
        musicSource.clip = combat;
        musicSource.Play();
    }
    public void StartMusic()
    {
        musicSource.clip = combat;
        musicSource.Play();
    }
}

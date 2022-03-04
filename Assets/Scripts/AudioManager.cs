using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;
    AudioSource audiosource;
    public AudioClip Shields;
    public AudioClip missileFiring;
    public AudioClip missileCruising;
    public AudioClip playerBullet;
    public AudioClip enemyBullet;
    public AudioClip powerupPickup;
    public AudioClip bossExplosion;
    public AudioClip bossDeathLaser;
    public AudioClip bossDeathExplosion;
    public AudioClip[] explosions;
    public AudioClip hit;
    

    // Start is called before the first frame update
    private void Start()
    {
        am = this;
        audiosource = GetComponent<AudioSource>();
    }

    public void PlaySoundOnce(AudioClip clip)
    {
        audiosource.clip = clip;
        audiosource.loop = false;
        audiosource.Play();
    }
    public void PlaySoundOnLoop(AudioClip clip)
    {
        audiosource.clip = clip;
        audiosource.loop = true;
        audiosource.Play();
    }
    public void PlayExplosion(AudioClip[] clips)
    {
        audiosource.clip = clips[Random.Range(0, clips.Length)];
        audiosource.loop = true;
        audiosource.Play();
    }
}

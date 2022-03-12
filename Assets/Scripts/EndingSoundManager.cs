using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSoundManager : MonoBehaviour
{
    public AudioClip spaceShipExplosion;
    public AudioClip cityLaser;
    [SerializeField] AudioSource explosionController;

    public void PlaySource(AudioClip clip)
    {
        if (!explosionController.isPlaying)
        {
            explosionController.clip = clip;
            explosionController.Play();
            Debug.Log("Playing");
        }
        
    }
}


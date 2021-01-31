using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] gunShotClips;
    public AudioClip[] gunHammerClips;
    public AudioClip[] gunTriggerClips;
    public AudioClip[] gunLoadClips;
    public AudioClip[] gunUnloadClips;
    public float volume = 0.5f;
    
    // Gun Shot
    public void PlayGunShot()
    {
        Debug.Log("Gun noises");
        audioSource.PlayOneShot(RandomGunShotClip(), volume);
    }

    AudioClip RandomGunShotClip()
    {
        return gunShotClips[UnityEngine.Random.Range(0, gunShotClips.Length)];
    }
    
    // Gun Trigger
    public void PlayGunTrigger()
    {
        Debug.Log("Gun noises");
        audioSource.PlayOneShot(RandomGunTriggerClip(), volume);
    }

    AudioClip RandomGunTriggerClip()
    {
        return gunTriggerClips[UnityEngine.Random.Range(0, gunTriggerClips.Length)];
    }
    
    // Gun Hammer
    public void PlayGunHammer()
    {
        Debug.Log("Gun noises");
        audioSource.PlayOneShot(gunHammerClips[0], volume);
    }

    // Gun Load
    public void PlayGunLoad()
    {
        Debug.Log("Gun noises");
        audioSource.PlayOneShot(gunLoadClips[0], volume);
    }

    // Gun Unload
    public void PlayGunUnload()
    {
        Debug.Log("Gun noises");
        audioSource.PlayOneShot(gunUnloadClips[0], volume);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    [SerializeField]
    private AudioClip startSound;

    public AudioClip StartSound
    {
        get
        {
            return startSound;
        }

    }

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStart()
    {
        audioSource.clip = StartSound;
        PlaySound();
    }


    private void PlaySound()
    {
        audioSource.Play();
    }

}

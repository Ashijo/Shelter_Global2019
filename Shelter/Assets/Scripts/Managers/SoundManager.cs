using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{

    #region Singleton
    private static SoundManager instance;


    private SoundManager()
    {

    }

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SoundManager();
            }
            return instance;
        }
    }

    #endregion


    private AudioSource explosionSource;
    private AudioSource musicSource;

    private AudioClip explosion;
    private AudioClip music;

    public void PlayMusic()
    {

        if (music == null)
        {
            music = Resources.Load("Arts/Sounds/Music") as AudioClip;
        }
        foreach (AudioSource audioSource in Camera.main.gameObject.GetComponents<AudioSource>())
        {
            if (audioSource.clip == null)
            {
                audioSource.clip = music;
            }

            if (audioSource.clip == music)
            {
                musicSource = audioSource;
                break;
            }
        }

        if (musicSource == null)
        {
            Camera.main.gameObject.AddComponent<AudioSource>();
            PlayMusic();
        }
        if (musicSource.isPlaying)
            musicSource.Stop();
        musicSource.Play();
    }

    public void StopMusic()
    {
    }

    public void PlayExplosion()
    {
        if (explosion == null)
        {
            explosion = Resources.Load("Arts/Sounds/Explosion") as AudioClip;
        }

        foreach (AudioSource audioSource in Camera.main.gameObject.GetComponents<AudioSource>())
        {
            if (audioSource.clip == null)
            {
                audioSource.clip = explosion;
            }

            if (audioSource.clip == explosion)
            {
                explosionSource = audioSource;
                break;
            }
        }

       

        if (explosionSource == null)
        {
            Camera.main.gameObject.AddComponent<AudioSource>();
            PlayExplosion();
        }

        if(explosionSource.isPlaying)
            explosionSource.Stop();
        explosionSource.Play();

    }

}

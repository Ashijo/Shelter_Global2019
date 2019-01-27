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

    public void PlayExplosion()
    {
        explosionSource = Camera.main.gameObject.GetComponent<AudioSource>();
        if (explosionSource == null)
        {
            Camera.main.gameObject.AddComponent<AudioSource>();
            explosionSource = Camera.main.gameObject.GetComponent<AudioSource>();
            explosionSource.transform.SetParent(Camera.main.transform);
            explosionSource.transform.localPosition = Vector3.zero;
            explosionSource.clip = Resources.Load("Arts/Sounds/Explosion")as AudioClip;
        }

        if(explosionSource.isPlaying)
            explosionSource.Stop();
        explosionSource.Play();

    }

}

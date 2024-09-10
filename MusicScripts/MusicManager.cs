using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;

    //Script para compartir la musica entre diferentes escenas.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource.Play();
    }
}

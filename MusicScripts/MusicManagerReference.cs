using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerReference : MonoBehaviour
{
    public string musicManagerName;
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = GameObject.Find(musicManagerName).GetComponent<MusicManager>();

        if (musicManager == null)
       {
            Debug.LogWarning("El elemento MusicManager no ha sido encontrado.");
       } 

       else
       {
            musicManager.StopMusic();
       }
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }
}

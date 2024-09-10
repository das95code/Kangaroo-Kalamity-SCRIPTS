using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPersistanceLvl2 : MonoBehaviour
{
    public static MusicPersistanceLvl2 instance2;
    private AudioSource audioSource2;
    private string currentScene2;
    private bool isPlayingMusic2;

    public string nombreEscena;

    private void Awake()
    {
        if (instance2 == null)
        {
            instance2 = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource2 = GetComponent<AudioSource>();
        isPlayingMusic2 = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newScene = scene.name;
        if (newScene != currentScene2)
        {
            if (newScene == nombreEscena)
            {
                // Si la nueva escena es el nivel, inicia la reproducción de la música desde cero
                isPlayingMusic2 = true;
                audioSource2.Stop();
                audioSource2.Play();
            }
            else
            {
                if (isPlayingMusic2)
                {
                    // Si la nueva escena no es el nivel y se está reproduciendo la música, detén la reproducción
                    isPlayingMusic2 = false;
                    audioSource2.Stop();
                }

                // Reproduce la música de la nueva escena
                AudioClip musicClip2 = GetMusicClipForScene(newScene);
                if (musicClip2 != null)
                {
                    audioSource2.clip = musicClip2;
                    audioSource2.Play();
                }
            }

            currentScene2 = newScene;
        }
    }

    private AudioClip GetMusicClipForScene(string sceneName)
    {
        return null;
    }
}

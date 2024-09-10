using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPersistanceLvl1 : MonoBehaviour
{
    public static MusicPersistanceLvl1 instance;
    private AudioSource audioSource;
    private string currentScene;
    private bool isPlayingMusic;

    public string nombreEscena;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        isPlayingMusic = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newScene = scene.name;
        if (newScene != currentScene)
        {
            if (newScene == nombreEscena)
            {
                // Si la nueva escena es el nivel, inicia la reproducción de la música desde cero
                isPlayingMusic = true;
                audioSource.Stop();
                audioSource.Play();
            }
            else
            {
                if (isPlayingMusic)
                {
                    // Si la nueva escena no es el nivel y se está reproduciendo la música, detén la reproducción
                    isPlayingMusic = false;
                    audioSource.Stop();
                }

                // Reproduce la música de la nueva escena
                AudioClip musicClip = GetMusicClipForScene(newScene);
                if (musicClip != null)
                {
                    audioSource.clip = musicClip;
                    audioSource.Play();
                }
            }

            currentScene = newScene;
        }
    }

    private AudioClip GetMusicClipForScene(string sceneName)
    {
        return null;
    }
}

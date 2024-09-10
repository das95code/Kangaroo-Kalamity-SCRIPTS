using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPersistanceLvl3 : MonoBehaviour
{
    public static MusicPersistanceLvl3 instance3; //Instancia estatica de la clase
    private AudioSource audioSource3; //Componente AudioSource para reproducir musica
    private string currentScene3; //Nombre de la escena actual
    private bool isPlayingMusic3; //Indica si la musica se esta reproduciendo o no


    public string nombreEscena; //Nombre de la escena que reproduce musica
    

    private void Awake()
    {
        if (instance3 == null)
        {
            instance3 = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource3 = GetComponent<AudioSource>();
        isPlayingMusic3 = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newScene = scene.name;
        if (newScene != currentScene3)
        {
            if (newScene == nombreEscena)
            {
                // Si la nueva escena es el nivel, inicia la reproduccion de la musica desde cero
                isPlayingMusic3 = true;
                audioSource3.Stop();
                audioSource3.Play();
            }
            else
            {
                if (isPlayingMusic3)
                {
                    // Si la nueva escena no es el nivel y se esta reproduciendo la musica, deten la reproduccion
                    isPlayingMusic3 = false;
                    audioSource3.Stop();
                }

                // Reproduce la musica de la nueva escena
                AudioClip musicClip = GetMusicClipForScene(newScene);
                if (musicClip != null)
                {
                    audioSource3.clip = musicClip;
                    audioSource3.Play();
                }
            }

            currentScene3 = newScene;
        }
    }

    private AudioClip GetMusicClipForScene(string sceneName)
    {
        return null;
    }
}

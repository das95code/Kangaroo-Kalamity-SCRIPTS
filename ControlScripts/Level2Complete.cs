using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Level2Complete : MonoBehaviour
{
    public Text levelCleared;
    public string nextSceneName = "LevelMap";

    public bool isCompleted = false; 

    public AudioSource win;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Comprueba si el objeto que colisionó es el jugador, por seguridad.
        if (collision.gameObject.CompareTag("Player"))
        {

            levelCleared.gameObject.SetActive(true);
            win.Play();
            isCompleted = true;
            PlayerPrefs.SetInt("Nivel2Completado", isCompleted ? 1 : 0);
            PlayerPrefs.Save();
            Invoke("ChangeScene", 2);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

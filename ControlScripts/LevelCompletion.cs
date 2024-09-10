using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletion : MonoBehaviour
{
    public Text levelCleared;
    public string nextSceneName = "LevelMap";

    public bool isCompleted = false; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Comprueba si el objeto que colisionó es el jugador, por seguridad.
        if (collision.gameObject.CompareTag("Player"))
        {

            levelCleared.gameObject.SetActive(true);
            isCompleted = true;
            PlayerPrefs.SetInt("NivelCompletado", isCompleted ? 1 : 0);
            PlayerPrefs.Save();
            Invoke("ChangeScene", 2);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

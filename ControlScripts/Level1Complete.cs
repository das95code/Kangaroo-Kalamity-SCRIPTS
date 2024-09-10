using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Level1Complete : MonoBehaviour
{
    public Text levelCleared; //Referencia al componente Text para mostrar el mensaje de nivel completado.
    public string nextSceneName = "LevelMap"; //Nombre de la siguiente escena a cargar.

    public bool isCompleted = false; //Indica si el nivel ha sido completado.

    public AudioSource win;

    private void OnCollisionEnter2D(Collision2D collision) //Si el jugador entra en colision...
    {
        //...comprueba si el objeto que colisiono es el jugador, por seguridad.
        if (collision.gameObject.CompareTag("Player"))
        {

            levelCleared.gameObject.SetActive(true); //El texto de nivel completado se vuelve visible.
            win.Play();
            isCompleted = true; //Se activa el booleano de completado, para el mapa de niveles.
            PlayerPrefs.SetInt("Nivel1Completado", isCompleted ? 1 : 0); //Guarda el booleano con PlayerPrefs.
            PlayerPrefs.Save(); //Guardamos.
            Invoke("ChangeScene", 2); //Invoca el metodo para cambiar la escena.
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName); //Metodo para cambiar la escena (con cierto retardo para que se vea el texto de victoria).
    }
}

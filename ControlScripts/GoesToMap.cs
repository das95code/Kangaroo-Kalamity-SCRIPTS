using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GoesToMap : MonoBehaviour
{
    public VideoPlayer creditos; //Referencia al objeto que contendra el video.
    public string nextSceneName = "LevelMap"; //Escena a la que cambiaremos.

    private bool isVideoStarted = false; //Con este booleano comprobaremos si el video ha comenzado a reproducirse.

    void Start()
    {
        creditos.started += OnVideoStarted; //En Start, agregamos un evento que se activa si el video comienza a reproducirse.
    }

    private void OnVideoStarted(VideoPlayer source)
    {
        isVideoStarted = true; //Si el video ha comenzado a reproducirse, activamos el booleano a true.
    }

    // Update is called once per frame
    private void Update()
    {
       if ((isVideoStarted && !creditos.isPlaying) || (Input.GetKey("space") || Input.GetKey(KeyCode.Return))) //Si el video ha empezado y ya no se esta reproduciendo...
       {
            SceneManager.LoadScene(nextSceneName); //...significa que ha terminado, y cargamos la siguiente escena.
       } 
    }
}

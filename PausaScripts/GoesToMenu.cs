using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GoesToMenu : MonoBehaviour
{
    public string targetSceneName; //Creamos la variable para seleccionar a mano la escena a la que queremos cambiar...

    public AudioSource start;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //Si pulsamos enter...
        {

            start.Play();

            SceneManager.LoadScene("MenuPrincipal"); //...cambiara de escena al menu principal.
        }
    }
}

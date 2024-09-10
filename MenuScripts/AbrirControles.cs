using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirControles : MonoBehaviour
{
    public void ToControles()
    {
        SceneManager.LoadScene("Controles");
    }
}

//El boton controles abrira la escena "Controles".

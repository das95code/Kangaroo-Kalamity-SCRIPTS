using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salir : MonoBehaviour
{
    public void CerrarJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}

//El boton "Salir" cerrara la aplicacion.

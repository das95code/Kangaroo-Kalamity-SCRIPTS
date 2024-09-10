using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarPartida : MonoBehaviour
{
    public void Cargar()
    {
        SceneManager.LoadScene("LevelMap"); 
    }
}

//El boton "Cargar Partida" abrira la escena del mapa de niveles.
//En caso de no tener partida guardada, lo abrira igualmente sin progreso.

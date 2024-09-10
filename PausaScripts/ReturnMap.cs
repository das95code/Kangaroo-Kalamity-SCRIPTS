using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMap : MonoBehaviour
{
    public void VolverAlMapa()
    {
        Time.timeScale = 1; // Reanudar el juego estableciendo el Time.timeScale a 1
        SceneManager.LoadScene("LevelMap");
    }
}

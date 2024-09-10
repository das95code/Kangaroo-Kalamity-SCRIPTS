using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState3 : MonoBehaviour
{
    public Sprite spriteAzul; //Sprite para el estado "completado" del nivel.
    public Sprite spriteRojo; //Sprite para el estado "no completado" del nivel.
    public Sprite spriteGris; //Sprite para el estado "bloqueado" del nivel.

    private SpriteRenderer spriteRenderer; //Referencia al SpriteRenderer del objeto.

    void Start()
    {
        //Cuando iniciamos, obtenemos el componente spriteRenderer asociado al objeto.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Esta vez utilizamos dos booleanos.
        //Con el booleano del nivel anterior comprobamos si el nivel esta bloqueado o no.
        bool isNivel2Completed = PlayerPrefs.GetInt("Nivel2Completado", 0) == 1;
        //Con el booleano del propio nivel, comprobamos si esta superado o no.
        bool isNivel3Completed = PlayerPrefs.GetInt("Nivel3Completado", 0) == 1;

        if (isNivel3Completed) //Si el nivel esta completado...
        {
            spriteRenderer.sprite = spriteAzul; //...el sprite sera azul.
        }

        else if (isNivel2Completed) //Si el nivel anterior esta completado (pero este no)...
        {
            spriteRenderer.sprite = spriteRojo; //...el sprite sera rojo.       
        }

        else //Para todo lo demas...
        {
            spriteRenderer.sprite = spriteGris; //...el sprite sera gris y el nivel estara bloqueado.
        }
    }
}
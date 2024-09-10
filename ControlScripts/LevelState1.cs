using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState1 : MonoBehaviour
{

    public Sprite spriteAzul; //Sprite para el estado "completado" del nivel.
    public Sprite spriteRojo; //Sprite para el estado "no completado" del nivel.

    private SpriteRenderer spriteRenderer; //Referencia al SpriteRenderer del objeto.

    void Start()
    {
        //Cuando iniciamos, obtenemos el componente spriteRenderer asociado al objeto.
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        //Utilizamos un booleano para comprobar si el nivel ha sido completado (mediante PlayerPrefs).
        //En caso de ser completado, este PlayerPrefs se guardara en la meta del nivel 1.
        bool isNivelCompleted = PlayerPrefs.GetInt("Nivel1Completado", 0) == 1;

        if (isNivelCompleted) //Si el nivel esta completado...
        {
            spriteRenderer.sprite = spriteAzul; //... el sprite sera azul.
        }

        else //En cualquier otro caso...
        {
            spriteRenderer.sprite = spriteRojo; //...el sprite sera rojo.
        }
    }
}

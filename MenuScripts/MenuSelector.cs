using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MenuSelector : MonoBehaviour
{

    public Button[] menuPrincipal; //Array de botones del menu principal.
    public Color selectedColor; //Color en el que parpadeara el texto.
    public Color normalColor; //Color del texto normal (blanco).
    public float blinkInterval; //Intervalo de parpadeo de color del texto.

    private int selectedIndex = 0; //Indice del boton seleccionado.
    private TextMeshProUGUI[] buttonTexts; //Array de textos de los botones.
    private Coroutine blinkCoroutine; //Esto referencia a la corrutina de parpadeo de color.

    public CanvasGroup dialogoNuevaPartida; //Hace referencia al dialogo de nueva partida.

    public AudioSource option;
    public AudioSource okey;

    void Start()
    {
        UpdateButtonColors(); //En Start actualizamos el color de los botones.
    }

    void Update()
    {   
        if (!dialogoNuevaPartida.interactable) //Esto se ejecutara siempre que el dialogo de nueva partida no este abierto.
        {
            //Logica de movimiento del selector de menu.
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) //Si pulsamos W o la flecha hacia arriba...
            {
                selectedIndex--; //...el indice seleccionado restara (en este caso, la seleccion subira al elemento anterior)
                if (selectedIndex < 0) //Si el indice se encuentra en 0 (es decir, la primera posicion)...
                    selectedIndex = menuPrincipal.Length - 1; //Iremos al ultimo boton de la lista.

                option.Play();

                UpdateButtonColors(); //Una vez terminado, actualizamos el color.

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) //Si pulsamos S o la flecha hacia abajo...
            {
                selectedIndex++; //...el indice sumara (la seleccion bajara al elemento siguiente)
                if (selectedIndex >= menuPrincipal.Length) //Y en caso de encontrarnos en el ultimo boton...
                    selectedIndex = 0; //...iremos al principio de la lista.

                option.Play();    

                UpdateButtonColors(); //De nuevo, actualizamos el color.
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) //Si pulsamos enter o espacio...
            {

                okey.Play();

                menuPrincipal[selectedIndex].onClick.Invoke(); //Invocara el metodo onClick del boton (la funcion que realiza).
            }
        }
    }

    //Metodo para actualizar el color de los botones.
    private void UpdateButtonColors()
    {
        if (buttonTexts == null || buttonTexts.Length == 0) 
        {
            //Aqui obtenemos los componentes TextMeshProUGUI de los botones.
            buttonTexts = new TextMeshProUGUI[menuPrincipal.Length];
            for (int i = 0; i < menuPrincipal.Length; i++)
            {
                buttonTexts[i] = menuPrincipal[i].GetComponentInChildren<TextMeshProUGUI>();
            }
        } 

        if (blinkCoroutine != null) //Si la corrutina de parpadeo es distinta de nulo...
        {
            StopCoroutine(blinkCoroutine); //detendremos la corrutina de parpadeo.
        }

        //Llegados a este punto, la corrutina de parpadeo de color se activara para el boton seleccionado.
        blinkCoroutine = StartCoroutine(BlinkButtonColor(selectedIndex));
    }

    //Metodo que se encarga del parpadeo de color del boton.
    private IEnumerator BlinkButtonColor(int buttonIndex)
    {
        for (int i = 0; i < buttonTexts.Length; i++) //Dentro de este bucle for, estableceremos el color del texto.
        {
            buttonTexts[i].color = (i == buttonIndex) ? selectedColor : normalColor;
        }

        yield return new WaitForSeconds(blinkInterval); //Que sera modificado en el tiempo estipulado (en mi caso, 0.5s)

        while (true) //Y por ultimo, en un bucle infinito, iniciaremos el parpadeo.
        {
            buttonTexts[buttonIndex].color = normalColor;
            yield return new WaitForSeconds(blinkInterval);

            buttonTexts[buttonIndex].color = selectedColor;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CuadroDialogo : MonoBehaviour
{

    public Button botonSi; //Referencia al boton "SI" del dialogo.
    public Button botonNo;  //Referencia al boton "NO" del dialogo.

    public Color selectedColor; //Color del texto seleccionado.
    public Color normalColor; //Color del texto normal.
    public float blinkInterval; //Intervalo de parpadeo del texto.

    private int selectedIndex = -1; //Indice de la opcion seleccionada.
    //Colocamos la opcion seleccionada fuera del dialogo para evitar un segundo missclick".

    private TextMeshProUGUI[] buttonTexts; //Array de textos de los botones.
    private Coroutine blinkCoroutine;  //Referencia a la corrutina de parpadeo de color de los botones.

    public CanvasGroup dialogCanvasGroup; //Componente CanvasGroup que abarca los componentes de este dialogo.

    public AudioSource option;
    public AudioSource okey;

    void Start()
    {
        UpdateButtonColors(); //En el Start, simplemente actualizamos el color de los botones.
    }

    void Update()
    {
        if (dialogCanvasGroup.interactable) //Con esto controlamos que las opciones del dialogo solo se puedan tocar si esta activo.
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) //Si pulsamos "A" o la flecha izquierda...
                {
                    selectedIndex = 0; //...nos moveremos al "SI"

                    option.Play();

                    UpdateButtonColors();
                }

                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) //Para la "D" y la flecha derecha...
                {
                    selectedIndex = 1; //...nos moveremos al "NO"

                    option.Play();

                    UpdateButtonColors();
                }

                //En este cuadro de dialogo no he implementado el retorno por el otro lado de la lista para 
                //asegurar lo maximo posible que el usuario esta completamente seguro de que quiere borrar su guardado.

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) //Con enter y espacio...
                {

                    okey.Play();

                    if (selectedIndex == 0) //...si estamos en la posicion 0...
                    {
                        BotonSi(); //...se ejecutara el metodo BotonSi.
                    }
                    else if (selectedIndex == 1) //Y si estamos en el indice 1...
                    {
                        BotonNo(); //...se ejecutara el boton no.
                    }
                }
        }
    }

    private void UpdateButtonColors() //Mismo metodo que en el menu principal.
    {
        //Obtenemos los componentes del TextMeshProUGUI.
        if (buttonTexts == null || buttonTexts.Length == 0)
        {
            buttonTexts = new TextMeshProUGUI[2];
            buttonTexts[0] = botonSi.GetComponentInChildren<TextMeshProUGUI>();
            buttonTexts[1] = botonNo.GetComponentInChildren<TextMeshProUGUI>();
        }

        //Detenemos la corrutina de parpadeo en caso de estar activa.
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        //Esto actualiza los colores de los botones segun la opcion seleccionada.
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            buttonTexts[i].color = (i == selectedIndex) ? selectedColor : normalColor;
        }
   
        if (selectedIndex != -1)
        {
            blinkCoroutine = StartCoroutine(BlinkButtonColor(selectedIndex));
        }
        
    }

    //El Enumerator funciona exactamente igual que el del menu principal.
    private IEnumerator BlinkButtonColor(int buttonIndex)
    {
        int otherButtonIndex = (buttonIndex == 0) ? 1 : 0;

        buttonTexts[buttonIndex].color = selectedColor;
        buttonTexts[otherButtonIndex].color = normalColor;

        while (true)
        {
            yield return new WaitForSeconds(blinkInterval);

            buttonTexts[buttonIndex].color = normalColor;
            yield return new WaitForSeconds(blinkInterval);

            buttonTexts[buttonIndex].color = selectedColor;
        }
    }

    public void BotonSi()
    {
        // Acciones a realizar al seleccionar "SI"
        Debug.Log("Seleccionaste Si");

        PlayerPrefs.DeleteAll();  //Borrara todos los datos guardados previamente.
        SceneManager.LoadScene("LevelMap"); //Cargara la escena del mapa de niveles.
    }

    public void BotonNo()
    {
        // Acciones a realizar al seleccionar "No"
        Debug.Log("Seleccionaste No");

        StartCoroutine(CerrarDialogo()); //Al seleccionar "NO", comenzara la corrutina cerrar dialogo.
    }

    private IEnumerator CerrarDialogo()
    {
    yield return new WaitForSeconds(0.1f); // Ligera demora antes de cerrar el dialogo (para no recoger el input del menu).

    // Cerrar el dialogo
    dialogCanvasGroup.alpha = 0; // Ocultar el dialogo
    dialogCanvasGroup.blocksRaycasts = false; // Desactivar interaccion con el dialogo
    dialogCanvasGroup.interactable = false; // Desactivar interaccion con el dialogo

    //Devolvemos el dialogo a origen
    selectedIndex = -1; // Reiniciar el indice a -1
    UpdateButtonColors();
    }
    
    public void AbrirDialogo() //Esto es para que el boton "Nueva Partida" pueda abrir este dialogo
    {
        // Mostrar el dialogo
        dialogCanvasGroup.alpha = 1; // Hacer visible el dialogo
        dialogCanvasGroup.blocksRaycasts = true; // Activar interaccion con el dialogo
        dialogCanvasGroup.interactable = true; // Activar interaccion con el dialogo

        //Devolvemos el dialogo a origen
        selectedIndex = -1; // Reiniciar el indice a -1
        UpdateButtonColors();
    }
}

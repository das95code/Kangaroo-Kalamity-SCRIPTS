using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class PauseSelector : MonoBehaviour
{

    //El PauseSelector funciona igual que el MenuSelector salvo ligeros cambios ya comentados que me obligan a crear un nuevo script.

    public Button[] menuPrincipal; //Array de botones del menu de pausa.
    public Color selectedColor; //Color del texto modificado del boton seleccionado.
    public Color normalColor; //Color del texto normal.
    public float blinkInterval; //Intervalo de parpadeo del boton seleccionado.

    private int selectedIndex = 0; //Indice del boton seleccionado.
    private TextMeshProUGUI[] buttonTexts; //Array de texto de los botones.
    private Coroutine blinkCoroutine; //Referencia a la corrutina de parpadeo.

    public AudioSource option;
    public AudioSource okey;

    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonColors();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) // Verificar si el script esta habilitado
        {
        return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = menuPrincipal.Length - 1;

            option.Play();    

            UpdateButtonColors();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex++;
            if (selectedIndex >= menuPrincipal.Length)
                selectedIndex = 0;

            option.Play();    

            UpdateButtonColors();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {

            okey.Play();

            menuPrincipal[selectedIndex].onClick.Invoke();
        }
    }

    private void UpdateButtonColors()
    {
        if (buttonTexts == null || buttonTexts.Length == 0)
        {
            buttonTexts = new TextMeshProUGUI[menuPrincipal.Length];
            for (int i = 0; i < menuPrincipal.Length; i++)
            {
                buttonTexts[i] = menuPrincipal[i].GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        blinkCoroutine = StartCoroutine(BlinkButtonColor(selectedIndex));
    }

    private IEnumerator BlinkButtonColor(int buttonIndex)
    {
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            buttonTexts[i].color = (i == buttonIndex) ? selectedColor : normalColor;
        }

        yield return new WaitForSecondsRealtime(blinkInterval); //Utilizamos WaitForSecondsRealTime para que el parpadeo no se vea afectado por el Time.timeScale = 0.

        while (true)
        {
            buttonTexts[buttonIndex].color = normalColor;
            yield return new WaitForSecondsRealtime(blinkInterval);

            buttonTexts[buttonIndex].color = selectedColor;
            yield return new WaitForSecondsRealtime(blinkInterval);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlink : MonoBehaviour
{
    public Transform targetTransform; //Referencia al Transform del objeto.
    public float blinkDuration = 1f; //Duracion total de cada parpadeo.
    public float minScale = 0.8f; //Escala minima del objeto durante el parpadeo.
    public float maxScale = 1.2f;   //Escala maxima del objeto durante el parpadeo.

    private bool isBlinking = false; //Booleano de control del parpadeo.
    private Vector3 originalScale; //Variable para la escala original del objeto.

    
    void Start()
    {
        originalScale = targetTransform.localScale; //Guardamos la variable original del objeto.
        StartBlink(); //Iniciamos el metodo StartBlink().
    }

    private void StartBlink() //Metodo para el parpadeo.
    {
        if (targetTransform != null && !isBlinking) //Verifica que haya un objeto asignado y que no haya comenzado el parpadeo.
        {
            isBlinking = true; //Controlamos que el parpadeo esta en curso.
            StartCoroutine(BlinkAnimation()); //Corrutina para la animacion de parpadeo.
        }
    }

    private IEnumerator BlinkAnimation()
    {

        while (true) //Creamos un ciclo infinito para que el parpadeo se repita constantemente.
        {
            float timeElapsed = 0f; //Tiempo transcurrido en el parpadeo actual.

            //Animacion de crecimiento
            while (timeElapsed < blinkDuration) //Realiza la animacion mientras no se alcance la duracion total del parpadeo.
            {
                float t = timeElapsed/blinkDuration; //Calculamos el progreso de la animacion en funcion del tiempo transcurrido.
                float scale = Mathf.Lerp(minScale, maxScale, t); //Calculamos la escala del objeto utilizando una interpolacion lineal.

                targetTransform.localScale = originalScale * scale; //Aplicamos la escala calculada al objeto...

                timeElapsed += Time.deltaTime; //Incrementamos el tiempo transcurrido usando el tiempo real del juego...
                yield return null; //...y esperamos un frame antes de reiniciar el bucle.
            }  

            //Para la animacion de decrecimiento, realizaremos el mismo proceso a la inversa...
            timeElapsed = 0f; //...siendo de especial importancia reiniciar el tiempo transcurrido par que suceda.

            while (timeElapsed < blinkDuration)
            {
                float t = timeElapsed/blinkDuration;
                float scale = Mathf.Lerp(maxScale, minScale, t);

                targetTransform.localScale = originalScale * scale;

                timeElapsed += Time.deltaTime;
                yield return null;
            }  
        }
    }
}

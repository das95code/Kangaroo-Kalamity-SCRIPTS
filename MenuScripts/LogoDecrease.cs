using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoDecrease : MonoBehaviour
{
    public Image image; //Referencia a la imagen.
    public float decreaseSpeed; //Velocidad de disminucion de la dimension de la imagne.
    public float targetSize; //La dimension objetivo a la que hay que disminuir.
    
    //Los puntos entre los que rebotara la imagen en la animacion.
    public Transform puntoA; 
    public Transform puntoB;


    public float initialMovementSpeed; //Velocidad inicial de movimiento (al decrecer).
    public float bucleSpeed; //Velocidad de movimiento para el bucle.

    private bool isMovingTowardsA; //Verificar si la imagen se esta moviendo al punto A.
    private Vector3 targetPosition; //Posicion objetivo del movimiento de la imagen.
    private float movementSpeed; //Velocidad de movimiento de la imagen.

    void Start()
    {
        isMovingTowardsA = true; //Activamos el booleano de control.
        targetPosition = puntoA.position; //Indicamos el punto A como posicion objetivo.
        movementSpeed = initialMovementSpeed; //Inciamos el movimiento del logo.
    }

    void Update()
    {
        //Para decrecer la dimension de la imagen
        if (image.transform.localScale.x > targetSize) 
        {
            float newSize = image.transform.localScale.x - (decreaseSpeed * Time.deltaTime); 
            newSize = Mathf.Max(newSize, targetSize);
            image.transform.localScale = new Vector3(newSize, newSize, 1f);
        }

        //Movemos la imagen al objetivo.
        image.transform.position = Vector3.MoveTowards(image.transform.position, targetPosition, movementSpeed * Time.deltaTime);

        // Verificar si hemos alcanzado el objetivo
        float distance = Vector3.Distance(image.transform.position, targetPosition);
        if (distance < 0.01f)
        {
            // Cambiar el objetivo del movimiento
            if (isMovingTowardsA)
            {
                targetPosition = puntoB.position;
                movementSpeed = bucleSpeed; //Aqui cambiamos la velocidad de movimiento a la del bucle, que sera mas lenta
            }
            else
            {
                targetPosition = puntoA.position;
            }

            isMovingTowardsA = !isMovingTowardsA;
        }
    }
}

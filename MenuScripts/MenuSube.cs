using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSube : MonoBehaviour
{
    public Transform puntoAMenu; //Punto hasta donde el menu se debe desplazar.
    public float speed; //Velocidad de movimiento del menu.

    private bool isMoving = false; //Booleano de control de movimiento.

    void Start()
    {
        isMoving = true; //En el Start, activaremos el booleano de movimiento.
    }

    void Update()
    {
        if (isMoving) //En el update, y siempre que el booleano este activo...
        {
            //...moveremos el menu...
            transform.position = Vector3.MoveTowards(transform.position, puntoAMenu.position, speed * Time.deltaTime);

            if (transform.position == puntoAMenu.position) //...y en caso de que llegue al punto A...
            {
                isMoving = false; //...dejara de moverse.
            }
        }
    }
}

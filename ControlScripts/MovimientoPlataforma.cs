using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{

    public GameObject Plataforma; //Referencia al objeto Plataforma.
    
    public Transform StartPoint; //Punto de inicio del movimiento.
    public Transform EndPoint; //Punto final del movimiento.

    public float Velocidad; //Velocidad de movimiento de la plataforma.

    private Vector3 MoverHacia; //Direccion hacia la que se mueve la plataforma.


    void Start()
    {
        MoverHacia = EndPoint.position; //Al comenzar, se movera al final de la plataforma.
    }

    void Update()
    {
        //La plataforma se movera hacia donde marque el vector MoverHacia
        Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if(Plataforma.transform.position == EndPoint.position) //Si la plataforma esta en la posicion final...
        {
            MoverHacia = StartPoint.position; //...el vector marcara la posicion inicial.
        }

        if(Plataforma.transform.position == StartPoint.position) //Si la plataforma esta en la posicion inicial...
        {
            MoverHacia = EndPoint.position; //el vector marcara la posicion final.
        }
    }

    //Metodo para que el jugador se mantenga sobre la plataforma
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (CheckGround.isGrounded == true) //Si el jugador esta pisando el suelo...
        {
            {
                //Sera hijo de la plataforma, por lo que no se movera.
                collision.collider.transform.SetParent(transform); 
            }
        }
    }

    //Si abandona la plataforma, dejara de ser hijo suyo y volvera a tener independencia.
    private void OnCollisionExit2D(Collision2D collision) 
    {
        collision.collider.transform.SetParent(null);
    }
}

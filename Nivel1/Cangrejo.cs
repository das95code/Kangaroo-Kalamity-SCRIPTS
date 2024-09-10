using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cangrejo : MonoBehaviour
{

    public GameObject cangrejo; //Referencia al objeto del cangrejo.

    public Transform StartPoint; //Punto de inicio del movimiento del cangrejo.
    public Transform EndPoint; //Punto final del movimiento del cangrejo.

    public float Velocidad; //Velocidad de moviento del cangrejo.

    private Vector3 MoverHacia; //Direccion hacia la que se mueve el sprite.

    public SpriteRenderer spriteRenderer; //Referencia al componente SpriteRenderer.

    //Metodo para poder "matar" al jugador al entrar en colision.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) //Si colisiona con el jugador...
        {
            Debug.Log("Player Damaged");
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged(); //...sufrira damage.
        }
    }

    void Start()
    {
        MoverHacia = EndPoint.position; //Nada mas comenzar, se movera a la posicion final.
    }

    void Update()
    {   //Mueve el cangrejo hacia la direccion que marque el vector MoverHacia a la velocidad indicada.
        cangrejo.transform.position = Vector3.MoveTowards(cangrejo.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if(cangrejo.transform.position == EndPoint.position) //Si el cangrejo llega a la posicion final...
        {
            MoverHacia = StartPoint.position; //... MoverHacia indicara la posicion inicial.
            spriteRenderer.flipX = true; //Y se volteara el sprite.
        }

        if(cangrejo.transform.position == StartPoint.position) //Si el cangrejo llega a la posicion inicial...
        {
            MoverHacia = EndPoint.position; //...mover hacia marcara la posicion final.
            spriteRenderer.flipX = false; //Y el sprite recupera su orientacion original.
        }
    }
}

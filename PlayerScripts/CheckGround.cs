using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    //Variable estatica que indica si el jugador esta tocando el suelo.
    public static bool isGrounded; 

    //Metodo que se llama cuando el collider del objeto entra en contacto con otro collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo")) // Comprobamos si el collider pertenece al tag "Enemigo"
        {
            isGrounded = false;
        }
        else{
            isGrounded = true; //Establecemos isGrounded como verdadero en caso de que sea asi.
        }
    }

    //Metodo que se llama cuando el collider del objeto sale de contacto con otro collider.
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false; //Establecemos isGrounded como verdadero en caso de que sea asi.
    }
}

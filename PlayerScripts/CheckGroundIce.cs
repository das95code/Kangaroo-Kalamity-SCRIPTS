using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundIce : MonoBehaviour
{
    //Variable estatica que indica si el jugador esta tocando el suelo.
    public static bool isGrounded; 
    public bool isIce = true; // Variable para indicar si el suelo es resbaladizo.

    //Metodo que se llama cuando el collider del objeto entra en contacto con otro collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo")) // Comprobamos si el collider pertenece al tag "Enemigo"
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true; //Establecemos isGrounded como verdadero en caso de que sea asi.

            if (isIce) // Verificar si el suelo es de hielo.
            {
                PlayerMoveOnIce playerMove = collision.GetComponent<PlayerMoveOnIce>();
                if (playerMove != null)
                {
                    playerMove.isSliding = true; // Establecer el estado de deslizamiento en verdadero.
                    playerMove.slideTimer = playerMove.slideDuration; // Reiniciar el temporizador de deslizamiento.
                }
            }
        }
    }

    //Metodo que se llama cuando el collider del objeto sale de contacto con otro collider.
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false; //Establecemos isGrounded como verdadero en caso de que sea asi.

        if (isIce) // Verificar si el suelo es de hielo.
        {
            PlayerMoveOnIce playerMove = collision.GetComponent<PlayerMoveOnIce>();
            if (playerMove != null)
            {
                playerMove.isSliding = false; // Establecer el estado de deslizamiento en falso al abandonar el suelo.
            }
        }
    }
}

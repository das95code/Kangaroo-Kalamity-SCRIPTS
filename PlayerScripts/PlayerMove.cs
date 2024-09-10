using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMove : MonoBehaviour
{

    public float runSpeed = 2; //Velocidad de movimiento lateral del jugador.
    public float jumpSpeed = 4; //Fuerza de salto del jugador.
    Rigidbody2D rb2D; //Referencia al componente Rigidbody2D del jugador (gravedad y fisicas).

    public bool betterJump = true; //Variable para habilitar saltos mejorados.
    public float fallMultiplier = 0.2f; //Multiplicador de caida para el salto.
    public float lowJumpMultiplier = 0.5f; //Multiplicador de salto bajo del jugador.
    public float walkJumpMultiplier = 10f; //Multiplicador de salto para caminar del jugador.

    public SpriteRenderer spriteRenderer; //Referencia al componente SpriteRenderer del jugador.
    public Animator animator; //Referencia al componente Animator del jugador.

    private bool juegoPausado = false; //Variable para controlar si el juego esta en pausa.
    public CanvasGroup menuPausa; //Referencia al CanvasGroup del menu pausa.
    public PauseSelector pauseSelector; //Referencia al script de seleccion del menu de pausa.

    public AudioSource jump;
    public AudioSource pause;

    void Start()
    {
     ReanudarJuego(); //Al igual que en el mapa, es importante reanudar el juego para evitar fallos con las pausas.
     rb2D = GetComponent<Rigidbody2D>(); //Obtenemos el componente Rigidbody2D.
    }

    //El FixedUpdate permite actualizaciones de fisica de forma mas sincronizada que con Update.
    void FixedUpdate() 
    {

        CheckForGround(); //Llamamos al metodo para identificar si estamos pisando el suelo.

            //Si no estamos pisando suelo y pulsamos la tecla "D" o la flecha derecha...
            if ((Input.GetKey("d") || Input.GetKey("right")) && CheckGround.isGrounded == false)
            {
                rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y); //...nos moveremos a la derecha...
                spriteRenderer.flipX = false; //...y el sprite se quedara en default, mirando a la derecha.
            }

            //Aplicamos la misma logica a la izquierda para las teclas "A" y la flecha izquierda.
            else if ((Input.GetKey("a") || Input.GetKey("left")) && CheckGround.isGrounded == false)
            {
                rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y); //-runspeed para desplazarnos hacia la izquierda.
                spriteRenderer.flipX = true; //Y, en este caso, si flipeamos el sprite para que mire a la izquierda.
            }
            
            else //En cualquier caso contrario, el personaje no se movera en el eje X.
            {
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            }

            //Si pulsamos espacio y estamos pisando suelo...
            if (Input.GetKey("space")  && CheckGround.isGrounded)
            {
                jump.Play();
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed); //El personaje saltara con la fuerza establecida.
            }

            if (CheckGround.isGrounded == false) //Establecemos la animacion de salto en caso de no estar pisando suelo...
            {
                animator.SetBool("JumpUp", true);
            }

            if (CheckGround.isGrounded == true) //...y la desactivamos en caso de que si.
            {
                animator.SetBool("JumpUp", false);
            }

            //Logica para los tres niveles de salto.
            if (betterJump)
            {
                if (rb2D.velocity.y < 0) //Si la velocidad del eje y llega a 0 (llegamos a lo alto)...
                {
                    rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime; //...entrara el multiplicador de caida.
                }

                if (rb2D.velocity.y > 0 && rb2D.velocity.y < 3 && !Input.GetKey("space")) //Si la velocidad es mayor que 0 y menor que 3 y no estamos presionando espacio....
                {
                    rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime; //...haremos un salto mas corto.
                }

                if (rb2D.velocity.y >= 3 && !Input.GetKey("space")) //Si la velocidad es mayor que 3 (acabamos de saltar) y no estamos presionando espacio...
                {
                    rb2D.velocity += Vector2.up * Physics2D.gravity.y * (walkJumpMultiplier) * Time.deltaTime; //...haremos un salto para caminar.
                }
            }
    }

    void Update() //He encapsulado los controles del menu de pausa en el update para no interferir con las fisicas del personaje en FixedUpdate.
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) //Si pulsamos la tecla "P" o escape...
        {

            pause.Play();

            if (!juegoPausado) //...y el juego no esta pausado...
            {
                PausarJuego(); //...lo pausaremos.
            }
            else //En cualquier otro caso...
            {
                ReanudarJuego(); //...lo reanudaremos.
            }
        }
    }

    private void CheckForGround() //Funcion CheckForGround para detectar si estamos pisando suelo.
    {
        if(CheckGround.isGrounded == true) //Si el booleano "isGrounded" de la clase CheckGround esta activo...
        {
            animator.SetBool("isGrounded", true); //...estableceremos el isGrounded en el animator.
                                                  //Esto es para controlar cuando entra la animacion de caida despues de llegar a lo alto del salto.
        }
        else //En caso contrariom desactivaremos el booleano.
        {
            animator.SetBool("isGrounded", false); 
            animator.SetFloat("jumpVelocity", rb2D.velocity.y);
        }
    }

    public void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0; // Pausar el juego estableciendo el Time.timeScale a 0.
        menuPausa.alpha = 1; // Hacer visible el menu de pausa.
        menuPausa.blocksRaycasts = true; // Activar raycast del menu de pausa.
        menuPausa.interactable = true; // Activar interaccion con el menu de pausa.

        pauseSelector.enabled = true; //Habilita el selector de pausa.
        
    }

    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1; // Reanudar el juego estableciendo el Time.timeScale a 1.
        menuPausa.alpha = 0; // Hacer visible el menu de pausa.
        menuPausa.blocksRaycasts = false; // Desactivar interaccion con el menu de pausa.
        menuPausa.interactable = false; // Desactivar interaccion con el menu de pausa.

        pauseSelector.enabled = false; //Deshabilita el selector de pausa.
    }
}

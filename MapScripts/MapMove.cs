using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MapMove : MonoBehaviour
{
    public GameObject canguro; //Referencia al objeto que controla el jugador en el mapa.

    public Transform Level1Entry; //Punto de entrada al nivel 1.
    public Transform Level2Entry; //Punto de entrada al nivel 2.
    public Transform Level3Entry; //Punto de entrada al nivel 3.

    public float Velocidad; //Velocidad de movimiento del sprite por el mapa.

    private Vector3 MoverHacia; //Posicion hacia la que se movera el jugador.

    public SpriteRenderer spriteRenderer; //Referencia al SpriteRenderer del objeto.

    private Vector3 playerStartPosition; // Posicion inicial del jugador en el mapa.

    //Para activar las pausas
    private bool juegoPausado = false; //Booleano de control de pausa.
    public CanvasGroup menuPausa; //Referencia al CanvasGroup que compone el menu de pausa.
    public PauseSelector pauseSelector; //Referencia al script del selector de pausa.

    //Para controlar si los controles del personaje están operativos (esto es para que no interfiera con el menu de pausa)
    private bool isControlsEnabled = true;

    public AudioSource enterLevel;
    public AudioSource pause;

    void Start()
    {   
        //Reanudamos el juego por si cambiamos de escena desde otro menu pausa activo y su booleano de control queda en true.
        ReanudarJuego(); 
        // Carga la posicion inicial del jugador para que le devuelva a la entrada del nivel que jugo.
        LoadPlayerPosition(); 
        MoverHacia = canguro.transform.position; // Establecemos la posicion inicial de movimiento.
    }

    void Update()
    {
        //Verificamos si el nivel 1 ha sido completado.
        bool isNivel1Completed = PlayerPrefs.GetInt("Nivel1Completado", 0) == 1;
        bool isNivel2Completed = PlayerPrefs.GetInt("Nivel2Completado", 0) == 1;
        bool isNivel3Completed = PlayerPrefs.GetInt("Nivel3Completado", 0) == 1;

        if (!juegoPausado && isControlsEnabled) //Si el juego no esta pausado y los controles estan habilitados...
        {
            //Este es un if de seguridad para poder entrar al nivel en caso de que el sprite de personaje quede atascado cerca.
            if (Vector2.Distance(canguro.transform.position, Level1Entry.position) < 0.1f)
            {
                //Si pulsamos la tecla "D" o la flecha derecha, y siempre que el nivel 1 este completado...
                if ((Input.GetKey("d") || Input.GetKey("right")) && isNivel1Completed)
                {
                    spriteRenderer.flipX = false; //...el sprite se orientara a "default" (en este caso, mirando a la derecha)...
                    MoverHacia = Level2Entry.position; //...y se desplazara hasta la entrada del nivel 2.
                }

                if (Input.GetKey("space") || Input.GetKey(KeyCode.Return)) //Si pulsamos enter o espacio en el nivel 1...
                {
                    enterLevel.Play();
                    SavePlayerPosition(); // ...guardara la posicion del avatar jugador...
                    SceneManager.LoadScene("Nivel1"); //...y entrara al nivel 1.
                }
            }

            if (Vector2.Distance(canguro.transform.position, Level2Entry.position) < 0.1f) //Si el avatar esta en la posicion del nivel 2....
            {
                if (Input.GetKey("a") || Input.GetKey("left")) //...podremos pulsar a o la flecha de la izquierda para volver al nivel 1.
                {
                    spriteRenderer.flipX = true; //En ese caso, el sprite se flipeara y mirara hacia la izquierda...
                    MoverHacia = Level1Entry.position; //...y se desplazara hasta la posicion de la entrada del nivel 1.
                }

                if ((Input.GetKey("d") || Input.GetKey("right")) && isNivel2Completed)
                {
                    spriteRenderer.flipX = false; //...el sprite se orientara a "default" (en este caso, mirando a la derecha)...
                    MoverHacia = Level3Entry.position; //...y se desplazara hasta la entrada del nivel 3.
                }

                if (Input.GetKey("space") || Input.GetKey(KeyCode.Return)) //De nuevo, si pulsamos enter o espacio...
                {
                    SavePlayerPosition(); // Guardara la posicion del avatar del jugador...
                    SceneManager.LoadScene("Nivel2"); //...y cargara el nivel 2.
                }
            }

            if (Vector2.Distance(canguro.transform.position, Level3Entry.position) < 0.1f) //Si el avatar esta en la posicion del nivel 3....
            {
                if (Input.GetKey("a") || Input.GetKey("left")) //...podremos pulsar a o la flecha de la izquierda para volver al nivel 2.
                {
                    spriteRenderer.flipX = true; //En ese caso, el sprite se flipeara y mirara hacia la izquierda...
                    MoverHacia = Level2Entry.position; //...y se desplazara hasta la posicion de la entrada del nivel 2.
                }

                if (Input.GetKey("space") || Input.GetKey(KeyCode.Return)) //De nuevo, si pulsamos enter o espacio...
                {
                    SavePlayerPosition(); // Guardara la posicion del avatar del jugador...
                    SceneManager.LoadScene("Nivel3"); //...y cargara el nivel 3.
                }
            }

            //Fuera de los ifs, moveremos al jugador hasta la posicion establecida.
            canguro.transform.position = Vector3.MoveTowards(canguro.transform.position, MoverHacia, Velocidad * Time.deltaTime);
        }

        //Si pulsamos "P" o "escape"...
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {

            pause.Play();

            if (!juegoPausado) //...y si el juego no esta pausado...
            {
                PausarJuego(); //... activara el metodo asociado a pausar el juego.
            }
            else //En caso contrario...
            {
                ReanudarJuego(); //...quitara la pausa.
            }
        }
    }

    //Este metodo se encarga de guardar la posicion del avatar del jugador en PlayerPrefs.
    private void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerPosX", canguro.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", canguro.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", canguro.transform.position.z);
        PlayerPrefs.Save();
    }

    //Este metodo sirve para cargar la posicion del avatar.
    private void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            canguro.transform.position = new Vector3(x, y, z);
        }
    }

    //Metodo para pausar el juego.
    public void PausarJuego()
    {
        juegoPausado = true; //Activa el booleano de control.
        Time.timeScale = 0; // Pausa el juego estableciendo el Time.timeScale a 0.
        menuPausa.alpha = 1; // Hace visible la pantalla de pausa.
        menuPausa.blocksRaycasts = true; // Activa el raycast de la pantalla de pausa.
        menuPausa.interactable = true; // Activa la interaccion con la pantalla de pausa.

        pauseSelector.enabled = true; //Habilita el selector de pausa.
        
    }

    //Metodo para reanudar el juego.
    public void ReanudarJuego()
    {
        juegoPausado = false; //Desactivamos el booleano de control.
        Time.timeScale = 1; // Reanuda el juego estableciendo el Time.timeScale a 1.
        menuPausa.alpha = 0; // Hace invisible la pantalla de pausa.
        menuPausa.blocksRaycasts = false; // Desactiva el raycast la pantalla de pausa.
        menuPausa.interactable = false; // Desactiva la interaccion con la pantalla de pausa.

        pauseSelector.enabled = false; //Deshabilita el selector de pausa.

        //Habilita los controles del avatar del jugador despues de 0.5 segundos.
        //Esto se hace para evitar pisar el input del menu con el del mapa.
        StartCoroutine(EnableControlsAfterDelay(0.5f)); 
    }

    //Metodo para habilitar o inhabilitar los controles del jugador
    private IEnumerator EnableControlsAfterDelay(float delay)
    {
        isControlsEnabled = false; // Deshabilita los controles

        yield return new WaitForSeconds(delay); //Espera una cantidad de segundos.

        isControlsEnabled = true; // Habilitar los controles
    }
}

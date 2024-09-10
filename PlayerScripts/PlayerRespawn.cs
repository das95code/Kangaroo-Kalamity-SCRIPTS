using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerRespawn : MonoBehaviour
{
    public Animator animator; // Referencia al componente Animator del jugador.

    public AudioSource dead;

    // Metodo para manejar la respuesta cuando un jugador es tocado por un enemigo.
    public void PlayerDamaged()
    {
        dead.Play();
        //El jugador respawneara despues de 0.2 segundos para que se pueda ver la animacion de muerte.
        StartCoroutine(RespawnAfterDelay(0.3f)); 
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        animator.Play("Damage"); // Reproduce la animacion de muerte del jugador.
        yield return new WaitForSeconds(delay); // Espera 0.2 segundos.

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena desde el principio.
    }
}

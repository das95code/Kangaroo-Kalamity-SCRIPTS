using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour
{

    private float waitedTime; //Tiempo esperado antes de atacar nuevamente
    public float waitTimeToAttack = 3; //Tiempo de espera para el proximo ataque

    public Animator animator; //Referencia al Animator del pinguino

    public GameObject bulletPrefab; //Prefab de la bala

    public Transform launchSpawnPoint; //Punto de spawn de la bala


    private void Start()
    {
        waitedTime = waitTimeToAttack; //Inicializar el tiempo de espera
    }

    private void Update()
    {
        if (waitedTime <= 0) //Reiniciar el tiempo de espera
        {
            waitedTime = waitTimeToAttack; //Reproducir la animacion de ataque del pinguino

            //Invocar el lanzamiento de la bala en diferentes momentos
            animator.Play("penguinattack");
            Invoke("LaunchBullet", 0.7f); //Crea 3 balas con una diferencia de 0.1s
            Invoke("LaunchBullet", 0.8f);
            Invoke("LaunchBullet", 0.9f);
        }
        
        else
        {
            waitedTime -= Time.deltaTime; //Actualizar el tiempo de espera
        }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Debug.Log("Player Damaged");
                collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged(); //Causa damage al jugador si colisiona con el
            }
        }

        public void LaunchBullet()
        {
            GameObject newBullet;

            newBullet = Instantiate(bulletPrefab, launchSpawnPoint.position, launchSpawnPoint.rotation);
        }
}

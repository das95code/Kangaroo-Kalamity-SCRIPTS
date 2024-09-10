using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2; //Velocidad del proyectil.
    public float lifetime = 1; //Tiempo de vida del proyectil.

    private void Start()
    {
        Destroy(gameObject, lifetime); //En el start, destruiremos el proyectil cuando pase su tiempo de vida.
    }

    private void Update()
    {
        transform.Translate(Vector2.down*speed*Time.deltaTime); //Indicaremos el movimiento que realizara el proyectil.
    }
}

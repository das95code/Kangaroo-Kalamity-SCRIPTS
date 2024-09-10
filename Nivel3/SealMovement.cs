using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealMovement : MonoBehaviour
{
    public GameObject seal;

    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;

    public SpriteRenderer spriteRenderer;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player Damaged");
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        seal.transform.position = Vector3.MoveTowards(seal.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if(seal.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
            spriteRenderer.flipX = true;
        }

        if(seal.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
            spriteRenderer.flipX = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : MonoBehaviour
{

    public GameObject chameleon;

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
        chameleon.transform.position = Vector3.MoveTowards(chameleon.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if(chameleon.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
            spriteRenderer.flipX = true;
        }

        if(chameleon.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
            spriteRenderer.flipX = false;
        }
    }
}

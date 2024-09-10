using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orca : MonoBehaviour
{
    //Mismo script que el del cangrejo.
    
    public GameObject orca;

    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        orca.transform.position = Vector3.MoveTowards(orca.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if(orca.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
            spriteRenderer.flipX = true;
        }

        if(orca.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player Damaged");
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged();
        }
    }

}

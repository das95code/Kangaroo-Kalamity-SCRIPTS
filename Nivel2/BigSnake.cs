using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSnake : MonoBehaviour
{

    public GameObject Snake;
    
    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;

    public SpriteRenderer spriteRenderer;

    private float waitTime;
    public float startWaitTime;


    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
        waitTime = startWaitTime;  
    }

    // Update is called once per frame
    void Update()
    {
        Snake.transform.position = Vector3.MoveTowards(Snake.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if (Snake.transform.position == StartPoint.position)
        {     
            MoverHacia = EndPoint.position;
            waitTime = startWaitTime;
        }

        if(Snake.transform.position == EndPoint.position)
        {
            if (waitTime <= 0)
            {
                MoverHacia = StartPoint.position;
                waitTime = startWaitTime;
            }
 
            else
            {
                waitTime -= Time.deltaTime;
            }
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

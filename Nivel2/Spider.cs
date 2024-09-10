using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    public GameObject spider;

    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;

    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        spider.transform.position = Vector3.MoveTowards(spider.transform.position, MoverHacia, Velocidad * Time.deltaTime);
    
        if(spider.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
        }

        if(spider.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
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

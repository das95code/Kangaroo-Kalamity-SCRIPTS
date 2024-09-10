using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacoPaco2 : MonoBehaviour
{

    public GameObject Plataforma;
    
    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;

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
        Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if (Plataforma.transform.position == EndPoint.position)
        {     
            MoverHacia = StartPoint.position;
        }

        if(Plataforma.transform.position == StartPoint.position)
        {
            if (waitTime <= 0)
            {
                MoverHacia = EndPoint.position;
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
        if (CheckGround.isGrounded == true)
        {
            {
                collision.collider.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) 
    {
        collision.collider.transform.SetParent(null);
    }
}

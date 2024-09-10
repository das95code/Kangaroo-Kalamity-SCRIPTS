using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlessie : MonoBehaviour
{

    public GameObject Plessie;

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
        Plessie.transform.position = Vector3.MoveTowards(Plessie.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if(Plessie.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
            spriteRenderer.flipX = true;
        }

        if(Plessie.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
            spriteRenderer.flipX = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMariposa : MonoBehaviour
{
    public GameObject Butterfly;

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
        Butterfly.transform.position = Vector3.MoveTowards(Butterfly.transform.position, MoverHacia, Velocidad * Time.deltaTime); 

       if(Butterfly.transform.position == EndPoint.position);
       {
            MoverHacia = StartPoint.position;
       }

       if(Butterfly.transform.position == StartPoint.position)
       {
            MoverHacia = EndPoint.position;
       }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveCredits : MonoBehaviour
{
        public GameObject Camera;

    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;


    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
    }

    void Update()
    {
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if(Camera.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
        }
    }
}

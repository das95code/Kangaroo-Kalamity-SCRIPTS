using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KangarooMove : MonoBehaviour
{

    public float runSpeed = 2;
    public float jumpSpeed = 3;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
        }

        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
        }

        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        } 
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundBounce : MonoBehaviour
{
    private float verticalBound = 8f;
    
    private float horizontalBound = 14f;
    private float speed = 5f;
    private Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if(transform.position.y > verticalBound)
        {
           rb.AddForce(Vector2.down * speed, ForceMode2D.Impulse);
        } 
        else if(transform.position.y < -verticalBound)
        {
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        }

        if(transform.position.x > horizontalBound)
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        }
        else if(transform.position.x < -horizontalBound)
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
    }
}

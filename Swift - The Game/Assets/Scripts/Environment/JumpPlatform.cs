using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [Range(0, 30)][SerializeField] private float jumpForce;

    void Awake()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            if(other.gameObject.GetComponent<Rigidbody2D>())
            {
                Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
                
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}

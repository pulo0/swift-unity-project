using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    public PlayerHealthController playerHealth;
    private Rigidbody2D playerRb;

    private float force = 15f;

    void Awake()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(20);
            playerRb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        else 
        {
            Destroy(other.gameObject);
        }
        
    }
}

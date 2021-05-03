using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Components variables")]
    private Rigidbody2D rb;
    public ParticleSystem destroyParticle;
    public ParticleSystem enemyDestroyParticle;
    private Collider2D playerCollider;
    private Collider2D bulletCollider;

    [Header("Forces")]
    private float bounceForce = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
        bulletCollider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        StartCoroutine(DestroyCountdown(2));   
        Physics2D.IgnoreCollision(playerCollider, bulletCollider);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.gameObject.CompareTag("Ground"))
        {
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }

        if(other.gameObject.name == "Enemy" || other.gameObject.name == "PoisonEnemy")
        {
            Destroy(other.gameObject);
            Instantiate(enemyDestroyParticle, other.transform.position, Quaternion.identity);
        }
    }
   
    public virtual IEnumerator DestroyCountdown(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
    }
}

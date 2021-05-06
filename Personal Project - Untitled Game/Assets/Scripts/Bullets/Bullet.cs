using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Components variables")]
    private Rigidbody2D rb;
    
    public ParticleSystem destroyParticle;
    public ParticleSystem[] enemyDestroyParticle;
    private Collider2D playerCollider;
    private Collider2D bulletCollider;

    [Header("Forces")]
    private float bounceForce = 5f;

    void Awake()
    {
        playerCollider = FindObjectOfType<PlayerController>().GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        StartCoroutine(DestroyCountdown(1f));   
        Physics2D.IgnoreCollision(playerCollider, bulletCollider);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            break;

            case "Enemy":
            Destroy(other.gameObject);
            Instantiate(enemyDestroyParticle[0], other.transform.position, Quaternion.identity);
            break;

            case "PoisonEnemy":
            Destroy(other.gameObject); 
            Instantiate(enemyDestroyParticle[1], other.transform.position, Quaternion.identity); 
            break;
        }
    }
   
    public virtual IEnumerator DestroyCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
    }
}

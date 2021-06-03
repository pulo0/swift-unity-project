using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Scripts variables")]
    private HealthController healthController;

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
        healthController = FindObjectOfType<Enemy>().GetComponent<HealthController>();
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
            healthController.TakeEnemyDamage(5f);
            Destroy(gameObject);
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            if(healthController.enemyCurrentHealth[0] <= 0)
            {
                Instantiate(enemyDestroyParticle[0], other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
            break;

            case "PoisonEnemy":
            healthController.TakePoisonDamage(5f);
            Destroy(gameObject);
            Instantiate(destroyParticle, transform.position, Quaternion.identity); 
            if(healthController.enemyCurrentHealth[1] <= 0)
            {
                Instantiate(enemyDestroyParticle[1], other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
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

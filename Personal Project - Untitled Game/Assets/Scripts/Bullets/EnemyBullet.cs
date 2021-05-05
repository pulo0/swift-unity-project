using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    [Header("Scripts variables")]
    private HealthController health;

    [Header("Components variables")]
    private Rigidbody2D rb2D;

    [Header("Forces")]
    private float bounceBulletForce = 5f;

    [Header("Damage related")]
    [SerializeField] private float damageToPlayer = 5f;

    [Header("Other")]
    private static int enemyLayer = 11;
    private static int enemyBulletLayer = 12;

    void Awake()
    {
        health = FindObjectOfType<PlayerController>().GetComponent<HealthController>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        StartCoroutine(DestroyCountdown(2));
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyBulletLayer);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.gameObject.CompareTag("Ground"))
        {
            rb2D.AddForce(Vector2.up * bounceBulletForce, ForceMode2D.Impulse);
        }

        if(other.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(damageToPlayer);
            Destroy(gameObject);
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
        }
    }

    public override IEnumerator DestroyCountdown(int time)
    {
        return base.DestroyCountdown(time);
    }
}

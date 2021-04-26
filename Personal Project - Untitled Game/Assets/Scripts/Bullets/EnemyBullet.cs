using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    [Header("Scripts variables")]
    private PlayerHealthController playerHealth;

    [Header("Components variables")]
    private Rigidbody2D rb2D;
    private Collider2D enemyBulletCollider;
    public Collider2D enemyCollider;

    [Header("Forces")]
    private float bounceBulletForce = 5f;

    void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealthController>();
        rb2D = GetComponent<Rigidbody2D>();
        enemyBulletCollider = GetComponent<Collider2D>(); 
    }
    
    void Update()
    {
        StartCoroutine(DestroyCountdown(2));
        Physics2D.IgnoreCollision(enemyCollider, enemyBulletCollider);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.gameObject.CompareTag("Ground"))
        {
            rb2D.AddForce(Vector2.up * bounceBulletForce, ForceMode2D.Impulse);
        }

        if(other.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(10);
        }
    }

    public override IEnumerator DestroyCountdown(int time)
    {
        return base.DestroyCountdown(time);
    }
}

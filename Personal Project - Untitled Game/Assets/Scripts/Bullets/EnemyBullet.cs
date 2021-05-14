using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public BulletType bulletType; 

    [Header("Scripts variables")]
    private HealthController health;

    [Header("Components variables")]
    private Rigidbody2D rb2D;

    [Header("Forces")]
    private float bounceBulletForce = 5f;

    [Header("Damage related")]
    [SerializeField] private float damageCooldown= 0.4f;
    [SerializeField] private float poisonDamageIncreasing = 5f;
    [SerializeField] private float damageToPlayer = 5f;
    private int damagePerTouch = 5;

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
        if(PoisonDamage(damageCooldown) != null)
        {
            poisonDamageIncreasing += Time.deltaTime;
        }

        StartCoroutine(DestroyCountdown(2));
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyBulletLayer);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
            rb2D.AddForce(Vector2.up * bounceBulletForce, ForceMode2D.Force);
            break;

            case "Player":
            switch (bulletType)
            {
                case BulletType.Normal:
                health.TakeDamage(damageToPlayer);
                Destroy(gameObject);
                break;

                case BulletType.Poison:
                StartCoroutine(PoisonDamage(damageCooldown));
                break;
            }

            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            break;          
        }
    }

    IEnumerator PoisonDamage(float damageCooldown)
    {
        float firstDamageValue = 5f;
        poisonDamageIncreasing = firstDamageValue;

        for (int i = 0; i < damagePerTouch; i++)
        {
            health.TakeDamage(poisonDamageIncreasing);
            yield return new WaitForSeconds(damageCooldown);
        }
        Destroy(gameObject);
        yield return null;
    }

    public override IEnumerator DestroyCountdown(float time)
    {
        return base.DestroyCountdown(time);
    }

    public enum BulletType
    {
        Normal, 
        Poison
    }
}

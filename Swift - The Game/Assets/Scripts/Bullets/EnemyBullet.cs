using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public BulletType bulletType; 

    [Header("Scripts variables")]
    private PlayerHealthCon playerHealth;

    [Header("Components variables")]
    private Rigidbody2D rb2D;

    [Header("Forces")]
    private float bounceBulletForce = 5f;

    [Header("Damage related")]
    [SerializeField] private float damageCooldown= 0.4f;
    [SerializeField] private int poisonDamage = 5;
    [SerializeField] private int damageToPlayer = 5;
    private int damagePerTouch = 5;

    [Header("Other")]
    private static int enemyLayer = 11;
    private static int enemyBulletLayer = 12;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerController>().GetComponent<PlayerHealthCon>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
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
                playerHealth.TakeDamage(damageToPlayer);
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
        int firstDamageValue = 5;
        poisonDamage = firstDamageValue;

        for (int i = 0; i < damagePerTouch; i++)
        {
            playerHealth.TakeDamage(poisonDamage);
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

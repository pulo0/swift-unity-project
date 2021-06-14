using System.Collections;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public BulletsType bulletsType;
    
    [Header("Components variables")]
    private Rigidbody2D rb2D;

    [Header("Forces")]
    private float bounceBulletForce = 5f;
    
    [Header("Other")]
    private const int EnemyLayer = 11;
    private const int EnemyBulletLayer = 12;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        StartCoroutine(DestroyCountdown(2));
        Physics2D.IgnoreLayerCollision(EnemyLayer, EnemyBulletLayer);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
            rb2D.AddForce(Vector2.up * bounceBulletForce, ForceMode2D.Force);
            break;

            case "Player":
                Instantiate(destroyParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
        }
    }
    
    private new IEnumerator DestroyCountdown(float time)
    {
        return base.DestroyCountdown(time);
    }
    
    public enum BulletsType
    {
        NormalBullet,
        PoisonBullet
    }
    
}

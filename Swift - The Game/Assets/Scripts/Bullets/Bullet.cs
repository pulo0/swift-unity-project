using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Components variables")]
    private Rigidbody2D rb;
    
    public ParticleSystem destroyParticle;
    private Collider2D playerCollider;
    private Collider2D bulletCollider;

    [Header("Forces")]
    private const float BounceForce = 5f;

    private void Awake()
    {
        playerCollider = FindObjectOfType<PlayerController>().GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();
    }
    
    private void Update()
    {
        StartCoroutine(DestroyCountdown(1f));   
        Physics2D.IgnoreCollision(playerCollider, bulletCollider);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                rb.AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);
                break;

            case "Enemy":
                Destroy(gameObject);
                Instantiate(destroyParticle, transform.position, Quaternion.identity);
                break;

            case "PoisonEnemy":
                Destroy(gameObject);
                Instantiate(destroyParticle, transform.position, Quaternion.identity);
                break;
        }
    }

   
    protected IEnumerator DestroyCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
    }
}

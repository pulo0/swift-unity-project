using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    private HealthController health;
    private Rigidbody2D playerRb;

    [SerializeField] private float lavaDamageCooldown = 0.4f;
    private float force = 15f;
    private float lavaDamage = 10f;
    private int damagePerTouch = 5;

    void Awake()
    {
        health = FindObjectOfType<HealthController>().GetComponent<HealthController>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update() 
    {

        Debug.Log("\nLava Damage: " + lavaDamage);

        if(LavaDamage(lavaDamageCooldown) != null)
        {
            lavaDamage += Time.deltaTime;
        }
            
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LavaDamage(lavaDamageCooldown));
            playerRb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        else 
        {
            Destroy(other.gameObject);
        }
        
    }

    IEnumerator LavaDamage(float damageCooldown)
    {
        float firstDamageValue = 10f;
        lavaDamage = firstDamageValue;

        for (int i = 0; i < damagePerTouch; i++)
        {
            health.TakeDamage(lavaDamage);
            yield return new WaitForSeconds(damageCooldown);
        }

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Difficulty;

public class Lava : MonoBehaviour
{

    private PlayerHealthCon playerHealth;
    private LevelDifficulty levelDifficulty;
    private Rigidbody2D playerRb;

    [SerializeField] private float lavaDamageCooldown = 0.4f;
    private const float force = 15f;
    private const int damagePerTouch = 5;
    private int lavaDamage = 10;
    
    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerController>().GetComponent<PlayerHealthCon>();
        levelDifficulty = GameObject.Find("LevelManager").GetComponent<LevelDifficulty>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
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

    private IEnumerator LavaDamage(float damageCooldown)
    {
        var firstDamageValue = levelDifficulty.lavaDamage;
        lavaDamage = firstDamageValue;

        for (var i = 0; i < damagePerTouch; i++)
        {
            playerHealth.TakeDamage(lavaDamage);
            yield return new WaitForSeconds(damageCooldown);
        }

        yield return null;
    }
}

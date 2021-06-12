using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lava : MonoBehaviour
{
    private PlayerHealthCon playerHealth;
    private LevelSetting levelSetting;
    private Rigidbody2D playerRb;

    [SerializeField] private float lavaDamageCooldown = 0.4f;
    private const float Force = 15f;
    private const int DamagePerTouch = 5;
    private int lavaDamage = 10;
    
    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerController>().GetComponent<PlayerHealthCon>();
        levelSetting = GameObject.Find("LevelManager").GetComponent<LevelSetting>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LavaDamage(lavaDamageCooldown));
            playerRb.AddForce(Vector2.up * Force, ForceMode2D.Impulse);
        }
        else
        {
            Destroy(other.gameObject);
        }
        
    }

    private IEnumerator LavaDamage(float damageCooldown)
    {
        var firstDamageValue = levelSetting.lavaDamage;
        lavaDamage = firstDamageValue;

        for (var i = 0; i < DamagePerTouch; i++)
        {
            playerHealth.TakeDamage(lavaDamage);
            yield return new WaitForSeconds(damageCooldown);
        }

        yield return null;
    }
}

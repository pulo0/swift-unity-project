using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerHealthCon : MonoBehaviour
{
    [Header("Health oriented")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Bullets & damage to it")]
    [SerializeField] private float damageCooldown = 0.4f;
    [SerializeField] private int poisonDamage = 5;
    [SerializeField] private int damageToPlayer = 5;
    private const int DamagePerTouch = 5;
    
    [Header("Damage oriented")]
    private const float TimeToChangeColor = 0.2f;
    private const float LerpSpeed = 1f; 
    private SpriteRenderer spriteRenderer;
    public Color colorOfObject;
    public TrailRenderer trail;
    public Gradient damageGradient;
    public Gradient normalGradient;

    [Header("Scripts")]
    private HealthBar healthBar;
    private PostProcessController postProcessController;

    private void Awake()
    {
        postProcessController = FindObjectOfType<PostProcessController>().GetComponent<PostProcessController>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<EnemyBullet>() != null)
        {
            EnemyBullet enemyBullet = other.gameObject.GetComponent<EnemyBullet>();
            
            switch (enemyBullet.bulletsType)
            {
                case EnemyBullet.BulletsType.NormalBullet:
                    TakeDamage(damageToPlayer);
                    break;
            
                case EnemyBullet.BulletsType.PoisonBullet:
                    StartCoroutine(PoisonDamage(damageCooldown));
                    break;
            }
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Health bar tracks now current health
        healthBar.SetHealth(currentHealth);
        
        StartCoroutine(ColorOnDamage(TimeToChangeColor)); 
        postProcessController.VignetteOnDamage();
    }
    
    private IEnumerator PoisonDamage(float damageCooldown)
    {
        const int firstDamageValue = 5;
        poisonDamage = firstDamageValue;

        for (var i = 0; i < DamagePerTouch; i++)
        {
            TakeDamage(poisonDamage);
            yield return new WaitForSeconds(damageCooldown);
        }
        yield return null;
    }

     private IEnumerator ColorOnDamage(float time)
    {
        //Color of player is lerp from his own color to red
        //Player's trail is set to red gradient
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.red, LerpSpeed);
        trail.colorGradient = damageGradient;

        //Wait x amount of time to change to normal color
        yield return new WaitForSeconds(time);

        //Color of player is lerp from damage color to his original color
        //Player's trail is set to his original gradient
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, colorOfObject, LerpSpeed);
        trail.colorGradient = normalGradient;
    }
}

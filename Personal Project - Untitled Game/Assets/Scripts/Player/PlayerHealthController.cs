using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [Header("Health oriented")]
    public float maxHealth = 100;
    public float currentHealth;

    [Header("Damage oriented")]
    private float timeToChangeColor = 0.2f;
    private float lerpSpeed = 1f;
    public SpriteRenderer spriteRenderer;
    public Color colorOfPlayer;
    public TrailRenderer trail;
    public Gradient damageGradient;
    public Gradient normalGradient;

    [Header("Scripts variables")]
    public HealthBar healthBar;
    
    void Start()
    {
        //Current Health is setted to max health
        //Then value of max health is setted im the health bar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Health bar tracks now current health
        healthBar.SetHealth(currentHealth);

        StartCoroutine(ColorOnDamage(timeToChangeColor));
    }

    IEnumerator ColorOnDamage(float time)
    {
        //Color of player is lerped from his own color to red
        //Player's trail is setted to red gradient
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.red, lerpSpeed);
        trail.colorGradient = damageGradient;

        //Wait x amount of time to change to normal color
        yield return new WaitForSeconds(time);

        //Color of player is lerped from damage color to his original color
        //Player's trail is setted to his original gradient
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, colorOfPlayer, lerpSpeed);
        trail.colorGradient = normalGradient;
    }
}

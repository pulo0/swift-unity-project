using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthCon : MonoBehaviour
{
    [Header("Health oriented")]
    [Space]
    public float maxHealth = 100;
    public float currentHealth;

    [Header("Damage oriented")]
    [Space]
    private float timeToChangeColor = 0.2f;
    private float lerpSpeed = 1f;
    private SpriteRenderer spriteRenderer;
    public Color colorOfObject;
    public TrailRenderer trail;
    public Gradient damageGradient;
    public Gradient normalGradient;

    [Header("Scripts")]
    [Space]
    private HealthBar healthBar;
    private PostProcessController postProcessController;

    void Awake()
    {
        postProcessController = FindObjectOfType<PostProcessController>().GetComponent<PostProcessController>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //Health bar tracks now current health
        healthBar.SetHealth(currentHealth);
        
        StartCoroutine(ColorOnDamage(timeToChangeColor, Color.red)); 
        postProcessController.IncreaseOnDamage();
    }

    public IEnumerator ColorOnDamage(float time, Color col)
    {
        //Color of player is lerped from his own color to red
        //Player's trail is setted to red gradient
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, col, lerpSpeed);
        trail.colorGradient = damageGradient;

        //Wait x amount of time to change to normal color
        yield return new WaitForSeconds(time);

        //Color of player is lerped from damage color to his original color
        //Player's trail is setted to his original gradient
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, colorOfObject, lerpSpeed);
        trail.colorGradient = normalGradient;
    }
}

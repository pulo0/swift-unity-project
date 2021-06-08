using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthCon : MonoBehaviour
{
    [Header("Health oriented")]
    [Space]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Damage oriented")]
    [Space]
    private const float TimeToChangeColor = 0.2f;
    private const float LerpSpeed = 1f; 
    private SpriteRenderer spriteRenderer;
    public Color colorOfObject;
    public TrailRenderer trail;
    public Gradient damageGradient;
    public Gradient normalGradient;

    [Header("Scripts")]
    [Space]
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Health bar tracks now current health
        healthBar.SetHealth(currentHealth);
        
        StartCoroutine(ColorOnDamage(TimeToChangeColor)); 
        postProcessController.VignetteOnDamage();
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

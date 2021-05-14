using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    [Header("Health oriented")]
    [Space]
    public float maxHealth = 100;
    public float currentHealth;
    public float[] enemyMaxHealth;
    public float[] enemyCurrentHealth;
    public bool canResetColor = false;

    [Header("Damage oriented")]
    [Space]
    private float timeToChangeColor = 0.2f;
    private float lerpSpeed = 1f;
    private SpriteRenderer spriteRenderer;
    public Color colorOfObject;
    public TrailRenderer trail;
    public Gradient damageGradient;
    public Gradient normalGradient;
    public Color col;

    [Header("Scripts variables")]
    [Space]
    public HealthBar healthBar;
    private PostProcessController postProcessController; 

    void Awake()
    {
        postProcessController = FindObjectOfType<PostProcessController>().GetComponent<PostProcessController>();
        SettingArrays();

        currentHealth = maxHealth;
        enemyCurrentHealth = enemyMaxHealth;
    }

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(gameObject.CompareTag("Player"))
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //Health bar tracks now current health
        healthBar.SetHealth(currentHealth);
        
        StartCoroutine(ColorOnDamage(timeToChangeColor, Color.red)); 
        postProcessController.IncreaseOnDamage();
    }

    public void TakeEnemyDamage(float enDamage)
    {
        enemyCurrentHealth[0] -= enDamage;
    }

    public void TakePoisonDamage(float enemyDamage)
    {
        enemyCurrentHealth[1] -= enemyDamage;
    }


    void SettingArrays()
    {
        enemyCurrentHealth = new float[2];
        enemyMaxHealth = new float[2];
        enemyMaxHealth[0] = 10f;
        enemyMaxHealth[1] = 15f;
    }

    public void ColorOnDamage(SpriteRenderer sr)
    {
        sr.color = new Color(1, 1, 1, 1f);
        canResetColor = true;
    }

    public void ResetColor(SpriteRenderer sr, Color col)
    {
        sr.color = col;
        canResetColor = false;
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

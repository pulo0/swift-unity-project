using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Difficulty;


public class Enemy : MonoBehaviour
{
    public LevelDifficulty levelDifficulty;

    [Header("Components variables")]
    private PlayerController playerController;
    public Transform target;
    public Transform aimTransform;
    public Transform lavaPrefabTransform;
    public Rigidbody2D enemyRb;
    public GameObject[] bullets;

    [Header("Particles")] 
    public ParticleSystem[] enemyDestroyParticle;
    
    [Header("Vectors")]
    public Vector3 direction;

    [Header("Health oriented")] [Space] 
    private SpriteRenderer spriteRenderer;
    private Color colorOfAnObject;
    protected const float TimeToChangeColor = 0.15f;
    private const float LerpSpeed = 1f;
    public float enemyMaxHealth;
    public float enemyCurrentHealth;

    [Header("Other")]
    public float stoppingDistance = 1f;
    private const float JumpForce = 6f;
    private const float FirstTime = 2;
    private float timer;
    public float timerForBullets;
    private int randomJump;
    private const int MaxRange = 5;

    public virtual void Awake()
    {
        enemyCurrentHealth = enemyMaxHealth;
        
        timer = FirstTime;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        levelDifficulty = GameObject.Find("LevelManager").GetComponent<LevelDifficulty>(); 
    }

    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOfAnObject = spriteRenderer.color;
    }

    public virtual void Update()
    {
        timer -= Time.deltaTime;
        timerForBullets += Time.deltaTime; 
        randomJump = Random.Range(0, MaxRange);
        
        direction = (target.position - transform.position).normalized;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        EnemyInteractions();
    }
    
    //Everything that enemy does will be in this method
    protected virtual void EnemyInteractions()
    {
        Movement(levelDifficulty.enemyMovementSpeed);
        Shoot(levelDifficulty.enemyShootDelay, levelDifficulty.enemyShootAmountOfBullets, levelDifficulty.enemyShootSpeed);
    }

    protected void Movement(float moveForce)
    {
        //Jump functions
        //If player double jumps and rng will pity on you then enemy will jump
        if(playerController.ExtraJumps == 0 && randomJump == 1 && timer <= 0)
        {
            enemyRb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            timer = FirstTime;
        }

        if(Vector2.Distance(transform.position, lavaPrefabTransform.position) < stoppingDistance && timer <= 0)
        {
            enemyRb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            timer = FirstTime;
        }   

        //Movement
        //If distance between enemy pos and the player is > than stoppingDistance or < than -stoppingDistance
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance || Vector2.Distance(transform.position, target.position) < -stoppingDistance)
        {
            //It applies force to move to the player
            enemyRb.AddForce(direction * moveForce);
        }
        else
        {
            enemyRb.AddForce(direction * moveForce);
        }
    }

    protected virtual void Shoot(float delay, int amountOfBullets, float bulletSpeed)
    {
        if (timerForBullets > delay)
        {
            for (var i = 0; i <= amountOfBullets; i++)
            {
                CreateEnemyBullet(0).GetComponent<Rigidbody2D>().AddForce((Vector2) direction * bulletSpeed + OffsetToShoot(), ForceMode2D.Impulse);
                timerForBullets = 0f;
            }
        }
    }
    
    protected virtual void TakeEnemyDamage(float enDamage)
    {
        enemyCurrentHealth -= enDamage;
        StartCoroutine(ColorOnDamage(TimeToChangeColor, Color.white));
        
        if (enemyCurrentHealth <= 0)
        {
            Instantiate(enemyDestroyParticle[0], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
    //When enemy takes damage then this routine will be activated
    protected virtual IEnumerator ColorOnDamage(float time ,Color enemyColOnDamage)
    {
        //Enemy sprite color lerp from its current color to whatever the variable will be set
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, enemyColOnDamage, LerpSpeed);
        
        //Wait x time to change to normal color
        yield return new WaitForSeconds(time);
        
        //Changes to original color
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, colorOfAnObject, LerpSpeed);
    }
    
    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        //If collided object has tag "Bullet", it will take damage to an enemy
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeEnemyDamage(5f);
        }
    }

    //This is used for other method that is called "OffsetToShoot"
    private static float RandomSpreadAngle(float rangeSpread)
    {
        //Gets a random value from a range
        var randomSpreadAngle = Random.Range(-rangeSpread, rangeSpread);
        return randomSpreadAngle;
    }

    protected static Vector2 OffsetToShoot()
    {
        //This is for random offset of a bullet in y axis
        var offsetToShoot = new Vector2(0, RandomSpreadAngle(10)); 
        return offsetToShoot;
    }

   protected GameObject CreateEnemyBullet(int index)
    {
        //This instantiate a new bullet with a variable as index
        var newBullet = Instantiate(bullets[index], transform.position + new Vector3(1, 0.5f, 0), aimTransform.rotation) as GameObject;
        return newBullet; 
    }
}
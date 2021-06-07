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
    
    [Header("Health oriented")]
    [Space]
    public float enemyMaxHealth;
    public float enemyCurrentHealth;

    [Header("Other")]
    public float stoppingDistance = 1f;
    private const float JumpForce = 6f;
    private const float FirstTime = 2;
    private float timer;
    public float timerForBullets;
    private float bulletSpeed;
    private int randomJump;
    private const int MaxRange = 5;

    public virtual void Awake()
    {
        enemyCurrentHealth = enemyMaxHealth;
        
        timer = FirstTime;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        levelDifficulty = GameObject.Find("LevelManager").GetComponent<LevelDifficulty>(); 
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

    protected virtual void EnemyInteractions()
    {
        Movement(levelDifficulty.enemyMovementSpeed);
        Shoot(levelDifficulty.enemyShootDelay, levelDifficulty.enemyShootAmountOfBullets, levelDifficulty.enemyShootSpeed);
    }

    protected void Movement(float moveForce)
    {
        //Jump functions
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
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance || Vector2.Distance(transform.position, target.position) < -stoppingDistance)
        {
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
    
    protected void TakeEnemyDamage(float enDamage)
    {
        enemyCurrentHealth -= enDamage;
    }
    
    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeEnemyDamage(5f);
            if (enemyCurrentHealth <= 0)
            {
                Instantiate(enemyDestroyParticle[0], transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private static float RandomSpreadAngle(float rangeSpread)
    {
        var randomSpreadAngle = Random.Range(-rangeSpread, rangeSpread);
        return randomSpreadAngle;
    }

    protected static Vector2 OffsetToShoot()
    {
        var offsetToShoot = new Vector2(0, RandomSpreadAngle(10)); 
        return offsetToShoot;
    }

   protected GameObject CreateEnemyBullet(int index)
    {
        var newBullet = Instantiate(bullets[index], transform.position + new Vector3(1, 0.5f, 0), aimTransform.rotation) as GameObject;
        return newBullet; 
    }
   
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Difficulty;


public class Enemy : MonoBehaviour
{
    public EnemyType enemyType; 
    public LevelDifficulty levelDifficulty;

    [Header("Components variables")]
    private PlayerController playerController;
    public Transform target;
    public Transform aimTransform;
    public Transform lavaPrefabTransform;
    public Rigidbody2D enemyRb;
    public GameObject[] bullets;

    [Header("Vectors")]
    private Vector3 direction;

    [Header("Other")]
    public float stoppingDistance = 1f;
    private const float JumpForce = 6f;
    private const float FirstTime = 2;
    private float timer;
    private float timerForBullets;
    private float bulletSpeed;
    private int randomJump;
    private const int MaxRange = 5;

    private void Awake()
    {
        timer = FirstTime;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        levelDifficulty = GameObject.Find("LevelManager").GetComponent<LevelDifficulty>(); 
    }
    
    private void Update()
    {
        timer -= Time.deltaTime;
        timerForBullets += Time.deltaTime; 
        randomJump = Random.Range(0, MaxRange);
        
        direction = (target.position - transform.position).normalized;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        switch(enemyType)
        {
            case EnemyType.NormalEn:
                Movement(levelDifficulty.enemyMovementSpeed);
                Shoot(levelDifficulty.enemyShootDelay, levelDifficulty.enemyShootAmountOfBullets, levelDifficulty.enemyShootSpeed);
                break;

            case EnemyType.PoisonEn:
                Movement(levelDifficulty.poisonEnMovementSpeed);
                Shoot(levelDifficulty.poisonEnShootDelay, levelDifficulty.poisonEnShootAmountOfBullets, levelDifficulty.poisonEnShootSpeed);
                StartCoroutine(ChangeEnemyGravity(levelDifficulty.gravModifier, levelDifficulty.timeToChangeGrav));
                break;
            
            default:
                Movement(levelDifficulty.enemyMovementSpeed);
                Shoot(levelDifficulty.enemyShootDelay, levelDifficulty.enemyShootAmountOfBullets, levelDifficulty.enemyShootSpeed);
                break;
        }
        
    }

    private void Movement(float moveForce)
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

    private void Shoot(float delay, int amountOfBullets, float bulletSpeed)
    {
        if (timerForBullets > delay)
        {
            for (var i = 0; i <= amountOfBullets; i++)
            {
                switch(enemyType)
                {
                    case EnemyType.NormalEn:
                        CreateEnemyBullet(0).GetComponent<Rigidbody2D>().AddForce((Vector2) direction * bulletSpeed + OffsetToShoot(), ForceMode2D.Impulse);
                        timerForBullets = 0f;
                        break;

                    case EnemyType.PoisonEn:
                        CreateEnemyBullet(1).GetComponent<Rigidbody2D>().AddForce((Vector2) direction * bulletSpeed + OffsetToShoot(), ForceMode2D.Impulse);
                        timerForBullets = 0f;
                        break;
                    
                    default:
                        CreateEnemyBullet(0).GetComponent<Rigidbody2D>().AddForce((Vector2) direction * bulletSpeed + OffsetToShoot(), ForceMode2D.Impulse);
                        timerForBullets = 0f;
                        break;
                }
            }
        }
    }

    private void EnemyTypeAmountOfBullets()
    {
        
    }

    private static float RandomSpreadAngle(float rangeSpread)
    {
        var randomSpreadAngle = Random.Range(-rangeSpread, rangeSpread);
        return randomSpreadAngle;
    }

    private static Vector2 OffsetToShoot()
    {
        var offsetToShoot = new Vector2(0, RandomSpreadAngle(10)); 
        return offsetToShoot;
    }

   private GameObject CreateEnemyBullet(int index)
    {
        var newBullet = Instantiate(bullets[index], transform.position + new Vector3(1, 0.5f, 0), aimTransform.rotation) as GameObject;
        return newBullet; 
    }

    private IEnumerator ChangeEnemyGravity(float gravityModifier, float timeToChangeGrav)
    {
        const float normalGrav = 2f;

        while (true)
        {
            yield return new WaitForSeconds(timeToChangeGrav);
            enemyRb.gravityScale = gravityModifier;

            yield return new WaitForSeconds(timeToChangeGrav);
            enemyRb.gravityScale = normalGrav;
        }
        
    }

    public enum EnemyType
    {
        NormalEn,
        PoisonEn
    }
}
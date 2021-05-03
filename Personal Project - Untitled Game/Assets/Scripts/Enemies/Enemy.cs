using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public EnemyType enemyType; 

    [Header("Components variables")]
    private PlayerController playerController;
    public Transform target;
    public Transform aimTransform;
    public Transform lavaPrefabTransfrom;
    public Rigidbody2D enemyRb;
    public GameObject bullet;

    [Header("Vectors")]
    private Vector3 direction;

    [Header("Other")]
    public float stoppingDistance = 1f;
    private float moveForce = 8f;
    private float jumpForce = 6f;
    private float time = 2;
    private float timer;
    private float timerForBullets;
    private float bulletSpeed;
    private int randomJump;

    void Awake()
    {
        timer = time;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        timerForBullets += Time.deltaTime; 
        randomJump = Random.Range(0, 5);
        
        direction = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Movement();

        switch(enemyType)
        {
            case EnemyType.NormalEn:
            Shoot(2, 0, 25);
            break;

            case EnemyType.PoisonEn:
            Shoot(3, 1, 15);
            break;
        }
        
    }

    void Movement()
    {
        //Jump functions
        if(playerController.ExtraJumps == 0 && randomJump == 1 && timer <= 0)
        {
            enemyRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            timer = time;
        }

        if(Vector2.Distance(transform.position, lavaPrefabTransfrom.position) < stoppingDistance && timer <= 0)
        {
            enemyRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            timer = time;
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

    void Shoot(float delay, int amountOfBullets, float bulletSpeed)
    {
        if (timerForBullets > delay)
        {
            for (int i = 0; i <= amountOfBullets; i++)
            {
                CreateEnemyBullet().GetComponent<Rigidbody2D>().AddForce((Vector2) direction * bulletSpeed + new Vector2(0, RandomSpreadAngle(10)), ForceMode2D.Impulse);
                timerForBullets = 0f;
            }
        }
    }

    private float RandomSpreadAngle(float rangeSpread)
    {
        float randomSpreadAngle = Random.Range(-rangeSpread, rangeSpread);
        return randomSpreadAngle;
    }

   private GameObject CreateEnemyBullet()
    {
        GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(1, 0.5f, 0), aimTransform.rotation) as GameObject;

        return newBullet;
    }

    public enum EnemyType
    {
        NormalEn,
        PoisonEn
    }
}

    


    

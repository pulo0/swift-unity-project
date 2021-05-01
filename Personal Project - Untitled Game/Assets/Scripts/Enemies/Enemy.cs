using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
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
    private float bulletSpeed = 25f;
    private int randomJump;

    public virtual void Awake()
    {
        timer = time;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        timer -= Time.deltaTime;
        timerForBullets += Time.deltaTime; 
        randomJump = Random.Range(0, 5);
        
        direction = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Movement();
        Shoot(2, 0);
    }

    public virtual void Movement()
    {
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

        if(Vector2.Distance(transform.position, target.position) > stoppingDistance || Vector2.Distance(transform.position, target.position) < -stoppingDistance)
        {
            enemyRb.AddForce(direction * moveForce);
        }
        else
        {
            enemyRb.AddForce(direction * moveForce);
        }
    }

    public virtual void Shoot(float delay, int amountOfBullets)
    {
        if (timerForBullets > delay)
        {
            for (int i = 0; i <= amountOfBullets; i++)
            {
                CreateEnemyBullet().GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                timerForBullets = 0f;
            }
        }
    }

    public virtual GameObject CreateEnemyBullet()
    {
        GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(1, 0.5f, 0), aimTransform.rotation) as GameObject;

        return newBullet;
    }
}

    


    

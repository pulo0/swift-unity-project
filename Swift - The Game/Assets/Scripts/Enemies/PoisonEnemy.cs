using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PoisonEnemy : Enemy
{
    public new void Awake()
    { 
        base.Awake();
    }

    public new void Start()
    {
        base.Start();
    }
    
    public new void Update()
    {
        base.Update();
    }

    protected override void EnemyInteractions()
    {
        Movement(levelSetting.poisonEnMovementSpeed);
        Shoot(levelSetting.poisonEnShootDelay, levelSetting.poisonEnShootAmountOfBullets, levelSetting.poisonEnShootSpeed);
        StartCoroutine(ChangeEnemyGravity(levelSetting.gravModifier, levelSetting.timeToChangeGrav));
    }

    private new void Movement(float moveForce)
    {
        base.Movement(moveForce);        
    }

    protected override void Shoot(float delay, int amountOfBullets, float bulletSpeed)
    {
        if (timerForBullets > delay)
        {
            for (var i = 0; i <= amountOfBullets; i++)
            {
                CreateEnemyBullet(1).GetComponent<Rigidbody2D>().AddForce((Vector2) direction * bulletSpeed + OffsetToShoot(), ForceMode2D.Impulse);
                timerForBullets = 0f;
            }
        }
    }

    protected override void TakeEnemyDamage(float enDamage)
    {
        enemyCurrentHealth -= enDamage;
        StartCoroutine(ColorOnDamage(TimeToChangeColor, Color.cyan));
        
        if (enemyCurrentHealth <= 0)
        {
            Instantiate(enemyDestroyParticle[1], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    protected override IEnumerator ColorOnDamage(float time, Color enemyColOnDamage)
    {
        return base.ColorOnDamage(time, Color.cyan);
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeEnemyDamage(5f);
        }
    }

    private new GameObject CreateEnemyBullet(int index)
    {
        return base.CreateEnemyBullet(index);
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
}

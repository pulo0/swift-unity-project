using System;
using System.Collections;
using System.Collections.Generic;
using Difficulty;
using UnityEngine;

public class PoisonEnemy : Enemy
{
    public new void Awake()
    { 
        base.Awake();
    }
    
    public new void Update()
    {
        base.Update();
    }

    protected override void EnemyInteractions()
    {
        Movement(levelDifficulty.poisonEnMovementSpeed);
        Shoot(levelDifficulty.poisonEnShootDelay, levelDifficulty.poisonEnShootAmountOfBullets, levelDifficulty.poisonEnShootSpeed);
        StartCoroutine(ChangeEnemyGravity(levelDifficulty.gravModifier, levelDifficulty.timeToChangeGrav));
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

    public new void TakeEnemyDamage(float enDamage)
    {
        base.TakeEnemyDamage(enDamage);
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeEnemyDamage(5f);
            
            if (enemyCurrentHealth <= 0)
            {
                Instantiate(enemyDestroyParticle[1], transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
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

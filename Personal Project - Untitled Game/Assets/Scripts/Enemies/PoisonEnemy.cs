using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEnemy : Enemy
{
    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Movement()
    {
        base.Movement();
    }

    public override void Shoot(float delay, int amountOfBullets)
    {
        base.Shoot(delay, amountOfBullets);
    }

    public override GameObject CreateEnemyBullet()
    {
        return base.CreateEnemyBullet();
    }
}

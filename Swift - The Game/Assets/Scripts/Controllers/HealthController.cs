using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    [Header("Health oriented")]
    [Space]
    public float[] enemyMaxHealth;
    public float[] enemyCurrentHealth;

    private void Awake()
    {
        SettingArrays();

        enemyCurrentHealth = enemyMaxHealth;
    }

    public void TakeEnemyDamage(float enDamage)
    {
        enemyCurrentHealth[0] -= enDamage;
    }

    public void TakePoisonDamage(float enemyDamage)
    {
        enemyCurrentHealth[1] -= enemyDamage;
    }

    private void SettingArrays()
    {
        enemyCurrentHealth = new float[2];
        enemyMaxHealth = new float[2];
        enemyMaxHealth[0] = 10f;
        enemyMaxHealth[1] = 15f;
    }
}

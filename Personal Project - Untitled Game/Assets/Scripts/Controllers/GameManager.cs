using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HealthController healthController;
    public PlayerAimGun playerShooting;

    public int enemyCount;
    
    void Start()
    {
        healthController = FindObjectOfType<PlayerController>().GetComponent<HealthController>();
        playerShooting = FindObjectOfType<PlayerController>().GetComponent<PlayerAimGun>();
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            playerShooting.canShoot = false;
        }

        ResetLevel();
    }

    void ResetLevel()
    {
        if(healthController.gameObject.CompareTag("Player"))
        {
            if(healthController.currentHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}

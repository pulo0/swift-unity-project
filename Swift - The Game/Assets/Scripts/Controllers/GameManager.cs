﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private HealthController healthController;
    private PlayerAimGun playerShooting;

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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
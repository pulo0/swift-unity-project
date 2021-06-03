using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerHealthCon playerHealth;
    private PlayerAimGun playerShooting;

    public int enemyCount;
    private const int maxLevelIndex = 2;
    
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerController>().GetComponent<PlayerHealthCon>();
        playerShooting = FindObjectOfType<PlayerController>().GetComponent<PlayerAimGun>();
    }

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            playerShooting.canShoot = false;
            StartCoroutine(NextLevel());
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

    private void ResetLevel()
    {
        if(playerHealth.currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private static IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(5);

        if(SceneManager.GetActiveScene().buildIndex < maxLevelIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

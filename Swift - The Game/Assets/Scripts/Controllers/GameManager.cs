using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerHealthCon playerHealth;
    private PlayerAimGun playerShooting;

    public int enemyCount;
    private static int maxLevelIndex = 1;
    
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerController>().GetComponent<PlayerHealthCon>();
        playerShooting = FindObjectOfType<PlayerController>().GetComponent<PlayerAimGun>();
    }

    void Update()
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

    void ResetLevel()
    {
        if(playerHealth.gameObject.CompareTag("Player"))
        {
            if(playerHealth.currentHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(5);

        if(SceneManager.GetActiveScene().buildIndex < maxLevelIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

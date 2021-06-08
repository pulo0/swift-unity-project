using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerHealthCon playerHealth;

    public int enemyCount;
    private const int MaxLevelIndex = 2;
    
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerController>().GetComponent<PlayerHealthCon>();
    }

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        
        switch (enemyCount)
        {
            case 0:
                StartCoroutine(NextLevel());
                break;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
        const int sec = 5;
        yield return new WaitForSeconds(sec);

        if(SceneManager.GetActiveScene().buildIndex < MaxLevelIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

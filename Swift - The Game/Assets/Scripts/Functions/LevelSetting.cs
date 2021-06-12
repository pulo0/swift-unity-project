using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetting : MonoBehaviour
{
    public LevelDifficulty levelDifficulty;
    
    public float enemyShootDelay;
    public int enemyShootAmountOfBullets;
    public float enemyShootSpeed;
    public float enemyMovementSpeed;

    
    public float gravModifier;
    public float timeToChangeGrav;
    public float camShakeDuration;
    public float camShakeMagnitude;
    
    public float poisonEnMovementSpeed;
    public float poisonEnShootDelay;
    public int poisonEnShootAmountOfBullets;
    public float poisonEnShootSpeed;  
    
    public float timeToShoot;
    
    public int lavaDamage;
    
    public int zoomValue;
    public float speedOfZoom;
    public float verticalBoundValue;
    public float horizontalBoundValue;

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        //Normal enemy stuff
        enemyShootDelay = levelDifficulty.enemyShootDelay;
        enemyShootSpeed = levelDifficulty.enemyShootSpeed;
        enemyShootAmountOfBullets = levelDifficulty.enemyShootAmountOfBullets;
        enemyMovementSpeed = levelDifficulty.enemyMovementSpeed;

        //Environment & camera stuff 
        gravModifier = levelDifficulty.gravModifier;
        timeToChangeGrav = levelDifficulty.timeToChangeGrav;
        camShakeDuration = levelDifficulty.camShakeDuration;
        camShakeMagnitude = levelDifficulty.camShakeMagnitude;

        //Poison Enemy stuff
        poisonEnMovementSpeed = levelDifficulty.poisonEnMovementSpeed;
        poisonEnShootDelay = levelDifficulty.poisonEnShootDelay;
        poisonEnShootAmountOfBullets = levelDifficulty.poisonEnShootAmountOfBullets;
        poisonEnShootSpeed = levelDifficulty.poisonEnShootSpeed;

        //Player stuff
        timeToShoot = levelDifficulty.timeToShoot;
        
        //Lava stuff
        lavaDamage = levelDifficulty.lavaDamage;
    
        //Zoom and bound variables
        zoomValue = levelDifficulty.zoomValue;
        speedOfZoom = levelDifficulty.speedOfZoom;
        verticalBoundValue = levelDifficulty.verticalBoundValue;
        horizontalBoundValue = levelDifficulty.horizontalBoundValue;
    }
    
}
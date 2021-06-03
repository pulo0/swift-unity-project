using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Difficulty
{
    public class LevelDifficulty : MonoBehaviour
    {
        [Header("Enemy Stuff")]
        [Space]
        public float enemyShootDelay;
        public int enemyShootAmountOfBullets;
        public float enemyShootSpeed;
        public float enemyMovementSpeed;

        [Header("Modifiers")]
        [Space]
        public float gravModifier;
        public float timeToChangeGrav;
        public bool allEnemiesHaveGravity;
        public float camShakeDuration;
        public float camShakeMagnitude;

        [Header("Poison Enemy Stuff")]
        [Space]
        public float poisonEnMovementSpeed;
        public float poisonEnShootDelay;
        public int poisonEnShootAmountOfBullets;
        public float poisonEnShootSpeed;  

        [Header("Player Stuff")]
        [Space]
        public float timeToShoot;

        [Header("Lava Stuff")]
        [Space]
        public int lavaDamage;

        [Header("Zoom & Bounds stuff")]
        [Space]
        public int zoomValue;
        public float speedOfZoom;
        public float verticalBoundValue;
        public float horizontalBoundValue;

    }
}


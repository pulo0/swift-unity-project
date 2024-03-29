﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAimGun : MonoBehaviour
{
    
    [Header("Scripts variables")]
    private CameraShake camShake;
    private LevelSetting levelSetting;
    private WeaponSwitching weaponSwitching;


    [Header("Components variables")]
    private Rigidbody2D playerRb;
    public GameObject[] bullets;
    public Transform aimTransform;
    private Camera cam;


    [Header("Rotation floats for the gun")]
    private const float RotationDuringSpin = 180f;
    private const float RotationToSpin = 90f;

    [Header("Animation stuff")] 
    public Animator bananaGunAnim;
    
    [Header("Bullet variables")]
    private const float BulletSpeed = 40f;
    [SerializeField] private float randomSpreadAngle;
    [SerializeField] private int amountOfBullets = 10;
    [SerializeField] private int maxAmmoAmount = 10;
    [SerializeField] private int currentAmount;


    [Header("Other")]
    private float timer = 0f;
    [Range(-5, 5)][SerializeField] private float recoil = 1f;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        cam = Camera.main;
        playerRb = GetComponent<Rigidbody2D>();
        camShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();
        levelSetting = GameObject.Find("LevelManager").GetComponent<LevelSetting>();
        weaponSwitching = GameObject.Find("Weapons").GetComponent<WeaponSwitching>();
        
        currentAmount = maxAmmoAmount;
    }
    
    private void Update()
    {
        timer += Time.deltaTime;

        //Puts mousePosition input as world point of a screen
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        var aimDirection = (mousePos - transform.position).normalized;

        //This calculate angle of aimDirection in 2D (Y and X axis)
        //Then multiplies to change it from radians to degrees 
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        //Spins a sprite of a gun whenever z axis hits 90 or -90 degrees
        if(angle >= RotationToSpin || angle <= -RotationToSpin)
        {
            aimTransform.eulerAngles = new Vector3(RotationDuringSpin, 0, -angle);
        }
        
        if(currentAmount <= 0)
        {
            //PLACEHOLDER
            currentAmount = maxAmmoAmount;
        }
        
        if(Input.GetMouseButtonDown(0) && timer > levelSetting.timeToShoot)
        {
            timer = 0;

            FireGun();

            StartCoroutine(camShake.Shake(levelSetting.camShakeDuration, levelSetting.camShakeMagnitude));
            playerRb.AddForce(-mousePos * recoil, ForceMode2D.Impulse);
        }
        
        //PLACEHOLDER
        switch (Input.inputString)
        {
            case "1":
                weaponSwitching.selectedWeapon = 0;
                break;
            
            case "2":
                weaponSwitching.selectedWeapon = 1;
                break;
            
            case "3":
                weaponSwitching.selectedWeapon = 2;
                break;
        }
    }
    
    private void FireGun()
    {
        //Mouse position just for a direction
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        var aimDirection = (mousePos - transform.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        switch (weaponSwitching.selectedWeapon)
        {
            //This is for normal "gun"
            case 0:
                BananaGunAnimationSetup();
                CreateBullet(0).GetComponent<Rigidbody2D>().velocity = aimDirection * BulletSpeed;
                break;

            //This is for shotgun
            case 1:
                //Subtracts from current ammo amount of bullets
                currentAmount -= amountOfBullets;

                if (currentAmount <= 0)
                    weaponSwitching.selectedWeapon = 0;

                for (var i = 0; i <= amountOfBullets; i++)
                {   
                    CreateBullet(0).GetComponent<Rigidbody2D>().AddForce((Vector2) aimDirection * BulletSpeed + new Vector2(0, RandomSpreadAngle(10)), ForceMode2D.Impulse);
                }
                break;
            
            case 2:
                const int maxValue = 4;
                const int minValue = 1;
                var randomBullet = Random.Range(minValue, maxValue);
                currentAmount--;

                if (currentAmount <= 0)
                    weaponSwitching.selectedWeapon = 0;
                
                CreateBullet(randomBullet).GetComponent<Rigidbody2D>().velocity = aimDirection * BulletSpeed;
                break;
        }
        
        
    }

    private void BananaGunAnimationSetup()
    {
        bananaGunAnim.SetBool("isShooting", true);
        StartCoroutine(AnimationToIdleRoutine());
    }
    

    private float RandomSpreadAngle(float rangeSpread)
    {
        randomSpreadAngle = Random.Range(-rangeSpread, rangeSpread);
        return randomSpreadAngle;
    }

    private IEnumerator AnimationToIdleRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        bananaGunAnim.SetBool("isShooting", false);
    }

    private GameObject CreateBullet(int i)
    {
        var offset = new Vector3(1, 0.5f, 0);
        var newBullet = Instantiate(bullets[i], transform.position + offset, aimTransform.rotation) as GameObject;

        return newBullet;
    }
    
}

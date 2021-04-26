using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimGun : MonoBehaviour
{
    [Header("Scripts variables")]
    private CameraShake camShake;

    [Header("Components variables")]
    private Rigidbody2D playerRb;
    public GameObject shotgunBulletPrefab;
    public Transform aimTransform;
    private Camera cam;

    [Header("Rotation floats for the gun")]
    private static float rotationDuringSpin = 180f;
    private static float rotationToSpin = 90f;

    [Header("Bullet variables")]
    private float bulletSpeed = 40f;
    [SerializeField] private float randomSpreadAngle;
    
    [SerializeField] private int amountOfBullets = 5;
    private GunsType gunsType;

    [Header("Other")]
    private float timer = 0f;
    [Range(-5, 5)][SerializeField] private float knockback = 1f;

    void Awake()
    {
        aimTransform = transform.Find("Aim");
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        playerRb = GetComponent<Rigidbody2D>();
        camShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();

        gunsType = GunsType.Pistol;
    }

    void Update()
    {
        timer += Time.deltaTime;

        //Puts mousePosition input as world point of a screen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - transform.position).normalized;

        //This calculate angle of aimDirection in 2D (Y and X axis)
        //Then multiplies to change it from radians to degrees 
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        if(angle >= rotationToSpin || angle <= -rotationToSpin)
        {
            aimTransform.eulerAngles = new Vector3(rotationDuringSpin, 0, -angle);
        }

        GunChange();

        if(Input.GetMouseButtonDown(0) && timer > 0.5f)
        {
            timer = 0;

            FireGun();

            StartCoroutine(camShake.Shake(0.1f, 0.2f));
            playerRb.AddForce(-mousePos * knockback, ForceMode2D.Impulse);
        }
    }

    void GunChange()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            gunsType = GunsType.Shotgun;
            //pistol.SetActive(false);
            //shotgun.SetActive(true);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            gunsType = GunsType.Pistol;
        }
    }

    void FireGun()
    {
        //Mouse position just for a direction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        //Offset of instatiated bullet
        Vector3 offset = new Vector3(1, 0.5f, 0);

        //This is for normal "gun"
        if(gunsType == GunsType.Pistol)
        {
            GameObject newShotgunBullet = Instantiate(shotgunBulletPrefab, transform.position + offset, aimTransform.rotation) as GameObject;
            newShotgunBullet.GetComponent<Rigidbody2D>().velocity = aimDirection * bulletSpeed;
        }
        //This is for shotgun
        else
        {
            for (int i = 0; i <= amountOfBullets; i++)
            {
                float rangeSpread = 10f;
                randomSpreadAngle = Random.Range(-rangeSpread, rangeSpread);
                GameObject newShotgunBullet = Instantiate(shotgunBulletPrefab, transform.position + offset, aimTransform.rotation) as GameObject;
                newShotgunBullet.GetComponent<Rigidbody2D>().AddForce((Vector2) aimDirection * bulletSpeed + new Vector2(0, randomSpreadAngle), ForceMode2D.Impulse);
            }
        }
    }

    private enum GunsType
    {
        Pistol, 
        Shotgun
    }

}

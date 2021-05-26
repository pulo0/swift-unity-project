using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoving : MonoBehaviour
{  
    [Header("Speed")]
    [SerializeField] private float exponentialSpeed;
    private float speed;

    [Header("Other")]
    [SerializeField] private float time;
    private float startPos;
    private Vector3 distance;

    [Header("Components variables")]
    public GameObject player;
    public Transform sensorTransform;

    void Start()
    {
        startPos = transform.position.x;

        speed = Random.Range(0.5f, 4f);
    }

    void Update()
    {
        //Variable that stores time in a way like timer
        time += Time.deltaTime;
        
        exponentialSpeed = Mathf.Lerp(0, 2f, speed);

        if(transform.position.x > distance.x)
        {
            distance = new Vector3(-XRange(), 0, 0);
            transform.Translate(Vector2.left * exponentialSpeed * Time.deltaTime);
        }
        else
        {
            distance = new Vector3(XRange(), 0, 0);
            transform.Translate(Vector2.right * exponentialSpeed * Time.deltaTime);
        }

        //Sensor position will be the same as platforms position
        sensorTransform.transform.position = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject == player)
        {
            //Player's transform is set as a child of a sensor on collision
            other.collider.transform.SetParent(sensorTransform.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject == player)
        {
            //Player's transform is set null on exit from collider
            other.collider.transform.SetParent(null);
        }
    }

    private float XRange()
    {
        int minRange = 5;
        int maxRange = 10;

        float offset = Random.Range(minRange, maxRange);
        float xRange;

        xRange = startPos + offset;

        return xRange;
    }
}

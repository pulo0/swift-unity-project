using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoving : MonoBehaviour
{  
    [Header("Speed")]
    [SerializeField] private float exponentialSpeed;
    private float speed = 0.5f;

    [Header("Other")]
    [SerializeField] private float time;
    private float xRange = 5f;
    private Vector3 distance;

    [Header("Components variables")]
    public GameObject player;
    public Transform sensorTransform;



    void Update()
    {
        //Variable that stores time in a way like timer
        time += Time.deltaTime;
        //Lerps from 0 to time so infinite with certain speed
        exponentialSpeed = Mathf.Lerp(0, 2f, speed);

        if(transform.position.x > distance.x)
        {
            distance = new Vector3(-xRange, 0, 0);
            transform.Translate(Vector2.left * exponentialSpeed * Time.deltaTime);
        }
        else
        {
            distance = new Vector3(xRange, 0, 0);
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
}

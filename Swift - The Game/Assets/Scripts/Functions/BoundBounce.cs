using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Difficulty;

public class BoundBounce : MonoBehaviour
{
    private LevelDifficulty levelDifficulty;
    
    private float verticalBound;
    
    private float horizontalBound;
    private float speed = 5f;
    private Rigidbody2D rb;
    
    
    void Awake()
    {
        levelDifficulty = GameObject.Find("LevelManager").GetComponent<LevelDifficulty>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        verticalBound = levelDifficulty.verticalBoundValue;
        horizontalBound = levelDifficulty.horizontalBoundValue; 
    }


    void Update()
    {
        //Vertical bound
        if(transform.position.y > verticalBound)
        {
           rb.AddForce(Vector2.down * speed, ForceMode2D.Impulse);
        } 
        else if(transform.position.y < -verticalBound)
        {
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        }

        //Horizontal bound (I'm sorry for if and else if)
        if(transform.position.x > horizontalBound)
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        }
        else if(transform.position.x < -horizontalBound)
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
    }
}

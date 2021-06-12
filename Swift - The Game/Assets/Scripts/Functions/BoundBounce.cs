using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundBounce : MonoBehaviour
{
    private LevelSetting levelSetting;
    
    private float verticalBound;
    
    private float horizontalBound;
    private const float Speed = 5f;
    private Rigidbody2D rb;
    
    
    private void Awake()
    {
        levelSetting = GameObject.Find("LevelManager").GetComponent<LevelSetting>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        verticalBound = levelSetting.verticalBoundValue;
        horizontalBound = levelSetting.horizontalBoundValue; 
    }


    private void Update()
    {
        //Vertical bound
        if(transform.position.y > verticalBound)
        {
           rb.AddForce(Vector2.down * Speed, ForceMode2D.Impulse);
        } 
        else if(transform.position.y < -verticalBound)
        {
            rb.AddForce(Vector2.up * Speed, ForceMode2D.Impulse);
        }

        //Horizontal bound (I'm sorry for if and else if)
        if(transform.position.x > horizontalBound)
        {
            rb.AddForce(Vector2.left * Speed, ForceMode2D.Impulse);
        }
        else if(transform.position.x < -horizontalBound)
        {
            rb.AddForce(Vector2.right * Speed, ForceMode2D.Impulse);
        }
    }
}

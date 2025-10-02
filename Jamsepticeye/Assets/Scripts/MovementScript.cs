using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MovementScript : MonoBehaviour
{
    float horizontalInput;
    public float movespeed = 5f;
    
    
    Rigidbody2D rb;
    public float jumpPower = 4f;
    bool isJumping => rb.velocity.y > 0.1f;
    bool isFalling => rb.velocity.y < -0.1f;
     public int maxJumps = 2;
     int remainingJumps; 
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingJumps = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        Flipsprite();



        if (Input.GetButtonDown("Jump") && remainingJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            remainingJumps--;
            isGrounded = false; 
            
        }
        
        
        

        
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            remainingJumps = maxJumps;

        }

        
    }
   


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * movespeed, rb.velocity.y);
    }

    void Flipsprite()
    {
        print(horizontalInput);
        if (horizontalInput < -1.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontalInput > 1.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    
    



}


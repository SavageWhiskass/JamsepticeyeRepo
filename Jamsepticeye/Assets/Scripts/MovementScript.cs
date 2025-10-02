using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    float horizontalInput;
    public float movespeed = 5f;
    
    Rigidbody2D rb;
    public float jumpPower = 4f;
    bool isJumping = false;
    private bool doubleJump;
    public LayerMask groundLayer;
    public int maxJumps = 2;
    int jumpsRemaining;
    

    public CoinManager cm; 
    
        // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseController.IsGamePaused)
        {
            rb.velocity = Vector2.zero;  //stop moving
        }

        horizontalInput = Input.GetAxis("Horizontal");
        Flipsprite();
        if (jumpsRemaining > 0)
        {



            if (Input.GetKeyDown("Space") && !isJumping)
            {

                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                isJumping = true;

            }
        }
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * movespeed, rb.velocity.y);
    }

    void Flipsprite()
    {
        print(horizontalInput);
        if (horizontalInput < -0.1f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontalInput > 0.1f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            cm.coincount++; 

        }
    }


}


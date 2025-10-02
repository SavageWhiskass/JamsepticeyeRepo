using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    float horizontalInput;
    public float movespeed = 5f;
    Rigidbody2D rb;
    public float jumpPower = 4f;
    bool jumpEnabled = true;
    bool holdingJump = false;
    bool hasDoubleJump = false;
    bool hasWallJump = false;
    float jumpCooldown = 0;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask ground;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Get left/right input and flip
        horizontalInput = Input.GetAxis("Horizontal");
        Flipsprite();

        //Get jumping input
        if(Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
        {
            holdingJump = true;
        }
        else
        {
            holdingJump = false;
            jumpEnabled = true;
        }

        //Jump cooldown
        if(jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(jumpEnabled && holdingJump && isGrounded() && jumpCooldown < 0.01f)
        {
            rb.velocity = new Vector2(horizontalInput * movespeed, jumpPower);
            jumpEnabled = false;
        }
        else if(!holdingJump && !isGrounded() && rb.velocity.y > 0.1f)
        {
            rb.velocity = new Vector2(horizontalInput * movespeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(horizontalInput * movespeed, rb.velocity.y);
        }
    }

    void Flipsprite()
    {
        if(horizontalInput < -0.01f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(horizontalInput > 0.01f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, ground))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded())
        {
            jumpCooldown = 0.1f;
        }
    }
}


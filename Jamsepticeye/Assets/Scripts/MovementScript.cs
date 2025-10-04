using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    float horizontalInput;
    public float movespeed;
    Rigidbody2D rb;
    public float jumpPower;
    bool isGrounded = false;
    bool jumpEnabled = true;
    bool holdingJump = false;
    public int maxJumps;
    int jumpsLeft;
    float jumpCooldown = 0;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask ground;
    public static MovementScript Instance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxJumps = 1;
        jumpsLeft = maxJumps;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //Check ground
        isGrounded = GroundCheck();

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
        if(jumpEnabled && holdingJump && (isGrounded || jumpsLeft > 0) && jumpCooldown < 0.01f)
        {
            rb.velocity = new Vector2(horizontalInput * movespeed, jumpPower);
            jumpEnabled = false;
            --jumpsLeft;
        }
        else if(!holdingJump && !isGrounded && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(horizontalInput * movespeed, rb.velocity.y / 2);
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

    public bool GroundCheck()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, ground))
        {
            if (!isGrounded)
            {
                jumpCooldown = 0.12f;
                jumpsLeft = maxJumps;
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}


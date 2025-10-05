using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    float horizontalInput;
    public int knockbackInput;
    public float movespeed;
    Rigidbody2D rb;
    Animator animator;
    public float jumpPower;
    bool isGrounded = false;
    bool jumpEnabled = true;
    bool holdingJump = false;
    public int maxJumps;
    int jumpsLeft;
    float jumpCooldown = 0;
    public Vector2 boxSize;
    public float castDistance;
    public Vector3 boxOffset;
    public LayerMask ground;
    public static MovementScript Instance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        maxJumps = 2;
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
        animator.SetFloat("verticalSpeed", rb.velocity.y);
        if(horizontalInput > 0.01 || horizontalInput < -0.01)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        //Get jumping input
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("jump");
            holdingJump = true;
        }
        else if (Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
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
            rb.velocity = new Vector2((horizontalInput * movespeed), jumpPower);
            jumpEnabled = false;
            --jumpsLeft;
        }
        else if(!holdingJump && !isGrounded && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2((horizontalInput * movespeed), (rb.velocity.y / 2));
        }
        else
        {
            rb.velocity = new Vector2((horizontalInput * movespeed), rb.velocity.y);
        }

    }

    void Flipsprite()
    {
        if(horizontalInput < -0.01f)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if(horizontalInput > 0.01f)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public bool GroundCheck()
    {
        if(Physics2D.BoxCast(transform.position + boxOffset, boxSize, 0, -transform.up, castDistance, ground))
        {
            if (!isGrounded)
            {
                jumpCooldown = 0.12f;
                jumpsLeft = maxJumps;
            }
            animator.SetBool("isGrounded", true);
            return true;
        }
        else
        {
            animator.SetBool("isGrounded", false);
            return false;
        }
    }

    //Ground check debug
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube((transform.position + boxOffset) - transform.up * castDistance, boxSize);
    //}
}


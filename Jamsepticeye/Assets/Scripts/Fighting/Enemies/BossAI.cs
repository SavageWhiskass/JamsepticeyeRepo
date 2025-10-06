using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossAI : Enemy
{
    Animator animator;
    bool allowMovement = true;
    bool readyToAttack = true;
    private float attackCurrentCooldown;
    private float attackCooldown = 3;
    private bool doDash = false;
    Transform player;
    public BoxCollider2D attackCollider;
    public BoxCollider2D screechCollider;
    float elapsed = 0.5f;

    public float speed;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackCurrentCooldown = attackCooldown;
        rb.drag = 1f;
    }

    void StopBoss()
    {
        allowMovement = false;
    }
    
    void StartBoss()
    {
        allowMovement = true;
        readyToAttack = true;
    }

    private void Update()
    {
        if (readyToAttack)
        {
            attackCurrentCooldown = attackCurrentCooldown - Time.deltaTime;
            float distance = Vector3.Distance(player.position, transform.position);
            if (attackCurrentCooldown <= 0f)
            {
                int randomAttack = Random.Range(1, 4);
                readyToAttack = false;

                switch (randomAttack)
                {
                    case 1:
                        TriggerDash();
                        break;
                    case 2:
                        TriggerSlash();
                        break;
                    case 3:
                        TriggerScream();
                        break;
                }
                attackCurrentCooldown = attackCooldown;
            }
        }
    }

    private void FixedUpdate()
    {
        if (allowMovement)
        {
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;
        }

        if (doDash)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            direction.Normalize();

            if(elapsed > 0.01f)
            {
                transform.position += direction * 10 * Time.deltaTime;
                elapsed -= Time.deltaTime;
            }
            else
            {
                doDash = false;
                elapsed = 0.5f;
            }
            
        }
    }

    void TriggerSlash()
    {
        animator.SetTrigger("slash");
    }

    public void SlashActivate()
    {
        attackCollider.enabled = true;
    }

    public void SlashDeactivate()
    {
        attackCollider.enabled = false;
    }

    void TriggerScream()
    {
        animator.SetTrigger("screech");
    }

    public void ScreechActivate()
    {
        screechCollider.enabled = true;
    }

    public void ScreechDeactivate()
    {
        screechCollider.enabled = false;
    }

    void TriggerDash()
    {
        animator.SetTrigger("lunge");
    }

    public void Dash()
    {
        doDash = true;
    }
}

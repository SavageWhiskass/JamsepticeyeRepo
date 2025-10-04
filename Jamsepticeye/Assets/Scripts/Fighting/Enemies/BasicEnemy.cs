using System.ComponentModel;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private float range = 2f;
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private float attack_cooldown = 3f;
    float attackCurrentCooldown;
    void Awake()
    {
        attackCollider.enabled = false;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void Attack()
    {
        // Add animation start here
        attackCollider.enabled = true;
    }

    public override void OnAttackHit(Collider2D collision)
    {
        collision.GetComponentInParent<PlayerDamage>()?.TakeDamage(1);
        attackCollider.enabled = false;
    }

    void Update()
    {
        attackCurrentCooldown = attackCurrentCooldown - Time.deltaTime;
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < range)
        {
            if (attackCurrentCooldown <= 0f)
            {
                Attack();
                attackCurrentCooldown = attack_cooldown;
            }
        }
    }
}
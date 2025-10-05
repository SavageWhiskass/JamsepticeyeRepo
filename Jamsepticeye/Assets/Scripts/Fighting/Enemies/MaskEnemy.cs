using System.ComponentModel;
using UnityEngine;

public class MaskEnemy : Enemy
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    Transform player;
    [SerializeField] private float range = 2f;
    CircleCollider2D attackCollider;
    [SerializeField] private float attack_cooldown = 3f;
    float attackCurrentCooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackCollider = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        
        Vector3 direction = (player.position - transform.position).normalized;

        if (direction.x < 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    void Attack()
    {
        // Add animation start here
        attackCollider.enabled = true;
    }

    public override void OnAttackHit(Collider2D collision)
    {
        
        attackCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.collider.GetComponentInParent<PlayerDamage>()?.TakeDamage(1);
            Destroy(gameObject);
        }
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
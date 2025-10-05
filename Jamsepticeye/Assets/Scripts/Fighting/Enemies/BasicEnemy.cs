using System.ComponentModel;
using UnityEngine;
using System.Collections;

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

    IEnumerator Attack()
    {
        // Add animation start here
        attackCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        attackCollider.enabled = false;
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
                StartCoroutine(Attack());
                attackCurrentCooldown = attack_cooldown;
            }
        }
    }
}
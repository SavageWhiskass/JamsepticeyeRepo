using System.ComponentModel;
using UnityEngine;
using System.Collections;

public class Boss : Enemy
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private float attack_cooldown = 2f;
    [SerializeField] private float dash_speed = 10000f;
    float attackCurrentCooldown;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 0.1f;
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
    }

    void Update()
    {
        attackCurrentCooldown = attackCurrentCooldown - Time.deltaTime;
        float distance = Vector3.Distance(player.position, transform.position);
        if (attackCurrentCooldown <= 0f)
        {
            //int randomAttack = UnityEngine.Random.Range(1, 4);
            int randomAttack = 1;
            switch (randomAttack){
                case 1:
                    Debug.Log("DASH");
                    StartCoroutine(Dash());
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            attackCurrentCooldown = attack_cooldown;
        }
    }

    IEnumerator Dash()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float dashTime = 1f;
        float elapsed = 0f;

        while (elapsed < dashTime)
        {
            transform.position += direction * dash_speed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private BoxCollider2D screamCollider;
    [SerializeField] private float attack_cooldown = 2f;
    [SerializeField] private float dash_speed = 10f;
    [SerializeField] private GameObject sludgePrefab;
    private bool screaming = false;
    float attackCurrentCooldown;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        attackCollider.enabled = false;
        //rb.drag = 0.1f;
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
        if (!screaming)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    IEnumerator Attack()
    {
        attackCollider.enabled = true;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(sludgePrefab, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        attackCollider.enabled = false;
    }

    IEnumerator Scream()
    {
        screaming = true;
        screamCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        screamCollider.enabled = false;
        screaming = false;
    }

    IEnumerator ScreamThenDash()
    {
        StartCoroutine(Scream());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Dash(0.5f, dash_speed / 2));
    }

    public override void OnAttackHit(Collider2D collision)
    {
        collision.GetComponentInParent<PlayerDamage>()?.TakeDamage(1);
        attackCollider.enabled = false;
    }

    
    public void OnScreamHit(Collider2D collision)
    {
        collision.GetComponentInParent<PlayerDamage>()?.StunPlayerTrigger(1f);
        screamCollider.enabled = false;
    }

    void Update()
    {
        attackCurrentCooldown = attackCurrentCooldown - Time.deltaTime;
        float distance = Vector3.Distance(player.position, transform.position);
        if (attackCurrentCooldown <= 0f)
        {
            int randomAttack = UnityEngine.Random.Range(1, 4);
            //int randomAttack = 1;
            switch (randomAttack){
                case 1:
                    Debug.Log("DASH");
                    StartCoroutine(Dash(0.5f, dash_speed));
                    break;
                case 2:
                    Debug.Log("Slash!");
                    StartCoroutine(Attack());
                    break;
                case 3:
                    Debug.Log("Scream!");
                    StartCoroutine(ScreamThenDash());
                    break;
            }
            attackCurrentCooldown = attack_cooldown;
        }
    }

    IEnumerator Dash(float dashTime, float dash_power)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        direction.Normalize();
        float elapsed = 0f;

        while (elapsed < dashTime)
        {
            transform.position += direction * dash_power * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
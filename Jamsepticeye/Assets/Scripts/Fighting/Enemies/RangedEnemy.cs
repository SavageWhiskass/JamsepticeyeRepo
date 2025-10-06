using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float shooting_cooldown = 3f;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private float range = 5f;
    private float detectionRange = 20f;
    public Transform firePoint;
    float fireCooldown;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
            rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
            rb.drag = 1f;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= detectionRange) {
            if (distance > range)
            {
                Vector3 direction = (player.position - transform.position).normalized;

                float step = speed * Time.fixedDeltaTime;
                float maxStep = distance - range;
                transform.position += direction * Mathf.Min(step, maxStep);
            }
        }

    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        fireCooldown = fireCooldown - Time.deltaTime;
        if (distance <= detectionRange)
        {
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = shooting_cooldown;
            }
        }
    }

    void Shoot()
    {
        Vector3 target = player.position;

        Vector3 direction = (target - firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        newBullet.GetComponent<Bullet>().Initialize(false, 1);
    }

}
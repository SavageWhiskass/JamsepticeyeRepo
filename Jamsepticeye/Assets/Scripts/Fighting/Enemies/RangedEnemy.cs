using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float shooting_cooldown = 3f;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;
    [SerializeField] private Transform player;
    public Transform firePoint;
    float fireCooldown;
    void Awake()
    {

    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > 7f)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void Update()
    {
        fireCooldown = fireCooldown - Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = shooting_cooldown;
        }
    }

    void Shoot()
    {
        Vector3 target = player.position;


        Vector3 direction = (target - firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        newBullet.GetComponent<Bullet>().Initialize(false);
    }

}
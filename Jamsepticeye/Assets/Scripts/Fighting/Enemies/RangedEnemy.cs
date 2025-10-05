using UnityEditor.Build.Reporting;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int shooting_cooldown = 3;
    public GameObject bulletPrefab;
    Transform player;
    Animator animator;
    [SerializeField] private float range = 5f;
    public Transform firePoint;
    float fireCooldown;
    bool inRange = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        fireCooldown = shooting_cooldown;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > range)
        {
            inRange = false;
            Vector3 direction = (player.position - transform.position).normalized;
            

            if (direction.x < 0)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            float step = speed * Time.fixedDeltaTime;
            float maxStep = distance - range;
            transform.position += direction * Mathf.Min(step, maxStep);
        }
        else
        {
            inRange = true;
        }
    }

    void Update()
    {
        if (inRange)
        {
            fireCooldown -= Time.deltaTime;
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        if (fireCooldown <= 0f)
        {
            animator.SetTrigger("cast");
            fireCooldown = shooting_cooldown;
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, new Quaternion(0, 0, 0, 0));
    }

}
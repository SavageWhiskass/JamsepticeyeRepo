using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    [SerializeField] private Transform player;

    void Awake()
    {

    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        if (distance > 2f)
        {
            //attack
        }
    }

    void Update()
    {

    }
}
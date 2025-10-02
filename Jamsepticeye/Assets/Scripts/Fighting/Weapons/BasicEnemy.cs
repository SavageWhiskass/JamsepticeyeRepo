using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private int hp = 10;
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
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void Update()
    {
        if (hp < 1)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}
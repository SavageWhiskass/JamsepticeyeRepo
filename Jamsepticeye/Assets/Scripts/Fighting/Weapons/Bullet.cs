using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public int damage = 1;
    public bool player_bullet;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = transform.right * speed;

        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    public void Initialize(bool friendly, int damageInput)
    {
        player_bullet = friendly;
        damage = damageInput;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (player_bullet)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //collision.gameObject.GetComponent<BasicEnemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
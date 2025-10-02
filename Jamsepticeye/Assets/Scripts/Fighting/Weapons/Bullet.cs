using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("huh?");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BasicEnemy>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
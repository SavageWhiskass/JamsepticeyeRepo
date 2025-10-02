using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private int hp = 10;

    private Rigidbody2D rb;

    void Awake()
    {

    }

    void Start()
    {

    }

    void FixedUpdate()
    {

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
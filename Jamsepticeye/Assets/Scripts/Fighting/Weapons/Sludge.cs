using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sludge : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = 10f;
    private Vector3 velocity;
    private bool hasLanded = false;

    void Start()
    {
        velocity = new Vector3(Random.Range(-3f, 3f), 8f, 0f);
    }

    void Update()
    {
        if (!hasLanded)
        {
            //velocity.y -= gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;

            if (transform.position.y <= 0f)
            {
                hasLanded = true;
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                velocity = Vector3.zero;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            // Damage player
            collision.gameObject.GetComponent<PlayerDamage>()?.StunPlayerTrigger(0.5f);
        }
    }
}
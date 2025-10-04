using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int hp = 10;

    // All enemies share this behavior
    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnAttackHit(Collider2D other) { }
}

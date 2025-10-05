using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int hp = 10;
    [SerializeField] private GameObject healthUpgradePrefab;
    [SerializeField] private GameObject manaUpgradePrefab;
    [SerializeField] private GameObject manaRegenUpgradePrefab;

    // All enemies share this behavior
    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            FindObjectOfType<PlayerStats>().RegisterKill();
            Destroy(gameObject);
            if (this.GetType().Name != "Boss")
            {
                int dropOrNot = UnityEngine.Random.Range(1, 101);
                if(dropOrNot < 20)
                {
                    int randomDrop = UnityEngine.Random.Range(1, 4);
                    //int randomAttack = 1;
                    switch (randomDrop)
                    {
                        case 1:
                            Instantiate(healthUpgradePrefab, transform.position, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(manaUpgradePrefab, transform.position, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(manaRegenUpgradePrefab, transform.position, Quaternion.identity);
                            break;
                    }
                }
            }
        }
    }

    public virtual void OnAttackHit(Collider2D other) { }
}

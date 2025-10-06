using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int hp = 10;
    private bool isDead = false;
    [SerializeField] protected GameObject healthUpgradePrefab;
    [SerializeField] protected GameObject manaUpgradePrefab;
    [SerializeField] protected GameObject manaRegenUpgradePrefab;

    // All enemies share this behavior
    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0 && !isDead)
        {
            isDead = true;
            FindObjectOfType<PlayerStats>().RegisterKill();
            if (this.GetType().Name != "Boss")
            {
                Debug.Log("OWWW IM DEADDDD");
                int dropOrNot = Random.Range(1, 101);
                if(dropOrNot < 50)
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
            Destroy(gameObject);
        }
    }

    public virtual void OnAttackHit(Collider2D other) { }
}

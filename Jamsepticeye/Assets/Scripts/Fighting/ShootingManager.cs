using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject powerBulletPrefab;
    public Transform firePoint;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("cast");
        }
    }

    public void CastBasic()
    {
        if (FindObjectOfType<PlayerStats>().currentMana > 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Vector3 direction = (mousePos - firePoint.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            
            var playerStats = GetComponentInParent<PlayerStats>();
            if (playerStats.has_big_blast)
            {
                GameObject newBullet = Instantiate(powerBulletPrefab, firePoint.position, rotation);
                newBullet.GetComponent<Bullet>().Initialize(true, 2);
            }
            else
            {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                newBullet.GetComponent<Bullet>().Initialize(true, 1);
            }
            
            FindObjectOfType<PlayerStats>().currentMana = FindObjectOfType<PlayerStats>().currentMana - 10;
        }
    }
}
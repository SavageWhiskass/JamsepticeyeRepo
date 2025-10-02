using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Awake()
    {
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Vector3 direction = (mousePos - firePoint.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Instantiate(bulletPrefab, firePoint.position, rotation);
        }
    }
}
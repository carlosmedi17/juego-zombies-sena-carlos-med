using UnityEngine;

public class weapon : MonoBehaviour 
{
    public Transform firePoint;
    public Transform aimPoint;
    public GameObject projectilePrefab;


    public float speed = 30f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    
    void Shoot()
    {
        if (firePoint  == null || aimPoint == null || projectilePrefab == null)
        {
            Debug.LogError("Asigna firePoint, aimPoint y projectile prefab en el inspector");
            return;
        }

        Vector3 direction = (aimPoint.position - firePoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction, speed);
        }
        
    }
}

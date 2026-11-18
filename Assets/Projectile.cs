using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public float lifeTime = 3f;

    private Vector3 direction;
    private float speed = 30f;

    public void SetDirection(Vector3 newDirection, float newSpeed)
    {
        direction = newDirection.normalized;
        speed = newSpeed;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player")) 
            return;

        EnemyAI1 enemy = other.GetComponentInParent<EnemyAI1>();

        if (enemy != null)
        {
            enemy.Die();
            Destroy(gameObject);
        }
    }
     
}

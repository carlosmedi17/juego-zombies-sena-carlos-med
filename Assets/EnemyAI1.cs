using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI1 : MonoBehaviour
{
    public float speed = 3f;
    public float respawnDelay = 3f;
    public float rotationOffset = 90f;

    [Header("enemigos adicionales")]
    public GameObject enemyPrefab;
    public float spawnRadius = 2f;

    private Transform player;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private Renderer[] renderers;
    private Collider[] colliders;
    private bool isDead = false;

    void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
            player = playerObject.transform;
        else
            Debug.LogError("No se encontro un objeto con la etiqueta Player");     
    }

    void Update()
    {
        if (player == null || isDead)
            return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if(direction.sqrMagnitude  > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, rotationOffset, 0f);

            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        TryRestartOnPlayer(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
       TryRestartOnPlayer(other.gameObject); 
    }

    void TryRestartOnPlayer(GameObject other)
    {
        if(other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        //Crear un enemigo adicional al eliminar este
        if (enemyPrefab != null)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;

            Vector3 newPosition = spawnPosition + new Vector3(offset.x, 0f, offset.y);

            GameObject newEnemy = Instantiate(enemyPrefab, newPosition, spawnRotation);

            //heredar la velocidad del enemigo que fue eliminado
            EnemyAI1 newEnemyAI1 = newEnemy.GetComponent<EnemyAI1>();

            if (newEnemyAI1 != null)
            {
                newEnemyAI1.speed = this.speed;
                newEnemyAI1.rotationOffset = this.rotationOffset;
                newEnemyAI1.respawnDelay = this.respawnDelay;
            }
        }
        else
        {
            Debug.LogWarning("Asigna el Enemy Prefab en el Inspector del enemigo");
        }

        foreach (Renderer r in renderers) 
            r.enabled = false;

        foreach (Collider c in colliders)
            c.enabled = false;

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        transform.position = spawnPosition;
        transform.rotation = spawnRotation;

        foreach (Renderer r in renderers)
            r.enabled = true;

        foreach (Collider c in colliders)
            c.enabled = true;

        isDead = false;
    }
}

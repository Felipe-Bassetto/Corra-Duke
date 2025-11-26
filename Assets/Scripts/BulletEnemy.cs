using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed;

    private Vector2 shootDirection; // direção fixa após o disparo

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // calcula a direção apenas UMA vez
            shootDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            // fallback: esquerda
            shootDirection = Vector2.left;
        }
    }

    void Update()
    {
        // move sempre na mesma direção
        transform.Translate(shootDirection * speed * Time.deltaTime);

        Destroy(gameObject, 8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

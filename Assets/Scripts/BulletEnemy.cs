using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{

    public float speed = 4f;
    public float maxFollowTime = 1.8f; 
    // tempo máximo que a bala persegue o jogador
    private float followTimer = 0;
    private Transform playerTransform;
    private bool isFollowing = true;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        
    }

    void Update()
    {
        if (isFollowing && playerTransform != null)
        {
            followTimer += Time.deltaTime;
            
            if (followTimer < maxFollowTime)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else 
            {
                isFollowing = false; // para de seguir 
            }
        }

        if (!isFollowing)
        {
            // bala vai para esquerda após para de seguir 
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            Destroy(gameObject, 8f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // destrói a bala sempre que acertar o jogador
        }
    }
}

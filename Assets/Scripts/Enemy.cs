using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxShots = 2; //número máximo de disparos
    public float speed = 3f;
    public float moveDuration = 1.5f;    
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootInterval = 2.5f;

    private int shotCount = 0; // quantos tiros já foram disparados
    private float moveTimer = 0f;
    private float shootTimer = 0f;
    private bool isMoving = true;
    private bool isAlive = true; 

    void Update()
    {
        if (!isAlive) return; // não deixa que a lógica rode após a morte

        if (isMoving)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            moveTimer += Time.deltaTime;

            if (moveTimer >= moveDuration)
            {
                isMoving = false;
            }
        }
        else
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            shotCount++; // conta mais um disparo
            
            if(shotCount >= maxShots)
            {
                LeaveScene(); // chama a função para sair da cena 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive) return; // não deixa acessar após morte (estava dando erro)

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            Die();
        }
    }

    public void TakeDamage()
    {
        if (!isAlive) return;
        Die();
    }

    private void Die()
    {
        isAlive = false;
        Destroy(gameObject);
    }

    void LeaveScene()
    {   // faz o inimigo sair andando pela direita
        isMoving = true;
        speed = Mathf.Abs(speed); // garante que a velocidade seja positiva
        Destroy(gameObject, 2f); // destrói o inimigo depois de x segundos
    }
}


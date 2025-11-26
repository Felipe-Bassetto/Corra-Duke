using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Animation")]
    private Animator anim;

    public int maxShots; //número máximo de disparos
    public float speed;
    public float moveDuration;    
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootInterval = 2.5f;

    private int shotCount = 0; // quantos tiros já foram disparados
    private float moveTimer = 0f;
    private float shootTimer = 0f;
    private bool isMoving = true;
    private bool isAlive = true;
    private bool isQuiting = false;
    private SoundManager soundManager;
    private Player player;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (soundManager == null)
        {
            soundManager = FindFirstObjectByType<SoundManager>();
        }

        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
    }

    void Update()
    {
        if (player.dead) return;

        if (!isAlive) return; // não deixa que a lógica rode após a morte

        if (isMoving)
        {
            if (isQuiting)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                moveTimer += Time.deltaTime;

                if (moveTimer >= moveDuration)
                {
                    isMoving = false;
                }
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
            anim.Play("Ganso Atirando");
            soundManager.SoundPlay(8);
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            shotCount++; // conta mais um disparo
            
            if(shotCount >= maxShots)
            {
                Debug.Log("Saindo");
                isMoving = true;
                StartCoroutine(LeaveScene()); // chama a função para sair da cena 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive) return; // não deixa acessar após morte (estava dando erro)

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            soundManager.SoundPlay(7);
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        isAlive = false;
        anim.Play("Ganso Morrendo");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public IEnumerator LeaveScene()
    {   // faz o inimigo sair andando pra cima
        isQuiting = true;
        anim.Play("Ganso Saindo");
        //speed = Mathf.Abs(speed); // garante que a velocidade seja positiva
        yield return new WaitForSeconds(3f);
        Destroy(gameObject); // destrói o inimigo depois de x segundos
    }
}


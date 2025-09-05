using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Definição de outros objetos
    public GameObject Bala;
    public GameObject gameOverPanel;
    public Transform Jogador;
    public Transform posicaoSpawn;
    private Rigidbody2D rb;

    //Defini��o de vari�veis
    public float distance = 2f; // distancia para calcular velocidade
    public int jumpForce = 8; // força do pulo
    public int maxHealth = 1; // vida maxima do jogado (a pensar)
    private int currentHealth; // vida atual
    private bool powerUpdActive = false; // Power up shield
    public int coinRound = 0; // Contador de moedas
    public bool jumpUp = true; // Pode pular
    public bool colliding = true; // Está colidindo

    // Definição de Listas
    public List<string> ListPlayerVunerable = new List<string>() { "Bomb", "EnemyBullet"};



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && jumpUp)
        {
            if (!colliding)
            {
                jumpUp = false;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Bala, posicaoSpawn.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Vida do jogador: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
       gameOverPanel.SetActive(true); // Ativa o painel antes de destruir o jogador
       GameObject.Find("GameOverManager").GetComponent<GameOverManager>().ShowGameOver();
       Debug.Log("Você perdeu");

       Destroy(gameObject); 
    }

    void OnBecameInvisible()
    {
        Die();
    }

    private void OnTriggerExit2D(Collider2D other) // Verificação se o jogador está tocando no chão
    {
        if (other.tag == "Ground" || other.tag == "GroupGround")
        {
            colliding = false;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        Debug.Log("teste");
        if (ListPlayerVunerable.Contains(obj.tag)) // Verifica se o player deve morrer ou não. E então executa a ação para cada tipo de objeto.
        {
            Debug.Log("teste");
            if (!powerUpdActive)
            {
                switch (obj.tag)
                {
                    case "EnemyBullet":
                    case "Bomb":
                        TakeDamage(1);
                        break;
                }
            }
        }
        else 
        {
            switch (obj.tag)
            {
                case "Ground":
                case "GroupGround":
                    colliding = true;
                    jumpUp = true;
                    break;
                case "Coin":
                    coinRound++;
                    break;
            }
        }
    }
}

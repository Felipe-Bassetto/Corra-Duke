using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Definição de outros objetos
    private Animator anim;
    public GameObject Bala;
    public GameObject gameOverPanel;
    public Transform Jogador;
    public Transform posicaoSpawn;
    private Rigidbody2D rb;
    public LevelManager level;
    public GameDb db;

    //Defini��o de vari�veis
    public float distance = 2f; // distancia para calcular velocidade
    public int jumpForce = 15; // força do pulo
    public int maxHealth = 1; // vida maxima do jogado (a pensar)
    private int currentHealth; // vida atual
    public int coinRound = 0; // Contador de moedas
    private bool powerUpdActive = false; // Power up shield
    public bool jumpUp = true; // Pode pular
    public bool colliding = true; // Está colidindo
    public string playerStatus = "Basic"; // Comando do jogador
    public float velocidadeVoo = 3f;
    public float coinMagnetRadius = 5f; // raio de atração
    public float coinMagnetForce = 10f; // velocidade que a moeda vem
    public bool doubleScoreActive = false; // controla o multiplicador
    private int finalScore;

    // Definição de Listas
    public List<string> listPlayerVunerable = new List<string>();

    public bool PowerUpdActive 
    {
        get { return powerUpdActive; }
        set { powerUpdActive = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1.8f;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStatus != "Flying")
        {
            if (Input.GetKeyDown(KeyCode.W) && jumpUp) // Comando W para pular
            {
                if (!colliding) // Caso esteja no ar
                {
                    jumpUp = false;
                    //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                else // Caso esteja no chão
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    Debug.Log("pulo");
                    anim.Play("Duke Jumping");
                }
            }
        }

        switch (playerStatus)
        {
            case "Gunner":
                if (Input.GetMouseButtonDown(0)) // Comando botão esquerdo para atirar
                {
                    Debug.Log("TIRO");
                    Instantiate(Bala, posicaoSpawn.position, Quaternion.identity);
                }
                break;
            case "Flying":

                rb.simulated = false;
                if (Input.GetMouseButton(0))
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (mousePos.y > transform.position.y)
                    {
                        transform.Translate(Vector2.up * velocidadeVoo * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(Vector2.down * velocidadeVoo * Time.deltaTime);
                    }
                }

                break;

            case "Destroyer":
               // Destruir todos os obstáculos à frente
               Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position + Vector3.right * 5f, 
               new Vector2(10f, 10f), 0f);
               foreach (Collider2D col in hits)
               {
                    if (col.CompareTag("Bomb"))
                    {
                    Destroy(col.gameObject);
                    }
               }
               break;

            case "Shield":
 
                PowerUpdActive = true; // Liga a invencibilidade 
                break;
            case "CoinMagnet":
               rb.simulated = true;
               doubleScoreActive = true;

              // Atrair moedas
              Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, coinMagnetRadius);
              foreach (Collider2D coin in coins) 
              {
                 if (coin.CompareTag("Coin"))
                 {
                    coin.transform.position = Vector2.MoveTowards(coin.transform.position,
                    transform.position,coinMagnetForce * Time.deltaTime);
                 }
              }
              break;

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

    public void alterStatus(string newStatus)
    {
        playerStatus = newStatus;

        if (newStatus != "GoldRush")
        {
            doubleScoreActive = false; // reseta multiplicador
        }
        
        // Resetar escala quando voltar ao normal
        if (newStatus == "Basic")
        {
             //transform.localScale = new Vector3(0.5f, 0.5f, 1f);
             rb.gravityScale = 1f;
        }
    }

    private void Die() // Função privada morte do jogador
    {
       gameOverPanel.SetActive(true); // Ativa o painel antes de destruir o jogador
       GameObject.Find("GameOverManager").GetComponent<GameOverManager>().ShowGameOver();
       Debug.Log("Você perdeu");
       
       finalScore = level.scoreMs; //Atualiza record se passou

       Configuracoes config = db.CarregarConfiguracoes();

       Progresso save = db.CarregarProgresso(config.Id);

       int record = save.ScoreRecord;
       int coins = save.Coins;

       if(record < finalScore);
       {
           record = finalScore;
       }

       coins += coinRound;

       db.SalvarProgresso(config.Id, record, finalScore);
       


       Destroy(gameObject); 
    }

    void OnBecameInvisible()
    {
       Die();
    }

    private void OnTriggerExit2D(Collider2D other) // Verificação se o jogador está tocando no chão
    {
        if (other.tag == "Collider")
        {
            colliding = false;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (listPlayerVunerable.Contains(obj.tag)) // Verifica se o player deve morrer ou não. E então executa a ação para cada tipo de objeto.
        {
            if (!PowerUpdActive)
            {
                switch (obj.tag)
                {
                    case "EnemyBullet":
                    case "Laser":
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
                case "Coin":
                    coinRound++;
                    break;
                case "Collider":
                    colliding = true;
                    jumpUp = true;
                    anim.Play("Running Duke");
                    break;
            }
        }
    }
}

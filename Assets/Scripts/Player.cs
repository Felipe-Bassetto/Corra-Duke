using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Animation")]
    private Animator anim;
    private bool pulando = false;
    private bool correndo = true;
    private bool pulandoDois = false;

    [Header("GameObjects")]
    public GameObject Bala;
    public GameObject gameOverPanel;
    public Transform Jogador;
    public Transform posicaoSpawn;
    private Rigidbody2D rb;
    public LevelManager level;
    public GameDb db;
    public GameObject pauseMenu;
    
    [Header("Pulo")]
    public float distance = 2f; // distancia para calcular velocidade
    public float jumpForce; // força do pulo
    public float secondJump; // força segundo pulo
    public float extraJumpForce; // força continua pulo
    public float maxJumpTime; // tempo maximo de pulo
    public float jumpTimeCounter;
    public bool jumpUp = true; // Pode pular
    private bool jumpPressed, jumpHeld, jumpRelease;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    enum PlayerState { Running, Jumping, DoubleJumping } 
    PlayerState state;
    PlayerState currentState;
    bool pausePressed;
    
    //Defini��o de vari�veis
    
    [Header("Player")]
    public int maxHealth = 1; // vida maxima do jogado (a pensar)
    private int currentHealth; // vida atual
    public string playerStatus = "Basic"; // Comando do jogador
    private bool dead = false;
    public List<string> listPlayerVunerable = new List<string>();

    [Header("PowerUps")]
    private bool powerUpdActive = false; // Power up shield
    public float coinMagnetRadius = 5f; // raio de atração
    public float coinMagnetForce = 10f; // velocidade que a moeda vem
    public bool doubleScoreActive = false; // controla o multiplicador
    public bool PowerUpdActive 
    {
        get { return powerUpdActive; }
        set { powerUpdActive = value; }
    }

    [Header("UI")]
    public int coinRound = 0; // Contador de moedas
    private int finalScore;
    public string deadReason;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1.8f;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        state = PlayerState.Running;
    }

    void FixedUpdate()
    {
        switch (playerStatus)
        {
            case "Basic":
            case "Destroyer":
                if (correndo) anim.CrossFade("Duke Running", 0.1f);
                if (pulando) anim.Play("Duke Jumping");
                break;
            case "Gunner":
                if (correndo) anim.CrossFade("Duke Gunner", 0.1f);
                if (pulando) anim.Play("Gunner Jump");
                break;

            case "CoinMagnet":
                if (correndo) anim.CrossFade("Magnetic Runner", 0.1f);
                if (pulando) anim.Play("Magnetic Jump");
                break;
        }

        if (pulandoDois) anim.CrossFade("Double Jump", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        pausePressed = Input.GetKeyDown(KeyCode.Escape);
        jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) ;
        jumpHeld = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W));
        jumpRelease = Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp (KeyCode.W) ; 

        
        if (pausePressed)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }

        if (jumpPressed && jumpUp) // Comando W para pular
        {
            if (isGrounded) // Caso esteja no chão
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                state = PlayerState.Jumping; 
            }
            else // Caso esteja no ar
            {
                jumpTimeCounter = 50f;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Zera a velocidade vertical ANTES de aplicar a nova força
                rb.AddForce(Vector2.up * secondJump, ForceMode2D.Impulse);
                jumpUp = false;
                state = PlayerState.DoubleJumping;
            }
        }
        
        if (isGrounded && jumpTimeCounter >= maxJumpTime)//!jumpHeld)
        {
            jumpUp = true;
            state = PlayerState.Running;
            jumpTimeCounter = 0f;
        }
        
        ChangeState(state);


        if (jumpHeld && !isGrounded && jumpUp)
        {
            if (jumpTimeCounter < maxJumpTime)
            {
                rb.AddForce(Vector2.up * extraJumpForce * Time.deltaTime, ForceMode2D.Force);
                jumpTimeCounter += Time.deltaTime;
            }
        }

        if (jumpRelease) jumpTimeCounter = 50f;

        switch (playerStatus)
        {
            case "Gunner":
                // Comando botão esquerdo para atirar
                if (Input.GetMouseButtonDown(0)) Instantiate(Bala, posicaoSpawn.position, Quaternion.identity);
                break;

            case "Destroyer":
               // Destruir todos os obstáculos à frente
               Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position + Vector3.right * 5f, 
               new Vector2(10f, 10f), 0f);
               foreach (Collider2D col in hits)
               {
                    if (col.CompareTag("Bomb")) Destroy(col.gameObject);
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

    void ChangeState(PlayerState newState)
    {
        if (currentState == newState) return;
        currentState = newState;

        switch (currentState)
        {
            case PlayerState.Running:
                pulando = false;
                pulandoDois = false;
                correndo = true;
                break;
            case PlayerState.Jumping:
                pulandoDois = false;
                correndo = false;
                pulando = true;
                break;
            case PlayerState.DoubleJumping:
                pulando = false;
                correndo = false;
                pulandoDois = true;
                break;
        }
    }

    public void alterStatus(string newStatus)
    {
        playerStatus = newStatus;

        if (newStatus != "GoldRush")doubleScoreActive = false; // reseta multiplicador
    }

    private void Die() // Função privada morte do jogador
    {
        if (dead) return; 

        dead = true;

       gameOverPanel.SetActive(true);// Ativa o painel antes de destruir o jogador
       GameObject.Find("GameOverPanel").GetComponent<GameOverManager>().ShowGameOver();

       
       
       finalScore = level.scoreMs; //Atualiza record se passou

       Configuracoes config = db.CarregarConfiguracoes();

       Progresso save = db.CarregarProgresso(config.Id);

       int record = save.ScoreRecord;
       int coins = save.Coins;

       if(record < finalScore) record = finalScore;  

       coins += coinRound;

       db.SalvarProgresso(config.Id, record, coins);

       Destroy(gameObject); 
    }

    void OnBecameInvisible()
    {
        deadReason = "Downfall";
        Die();
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
                        deadReason = "Enemy Bullet";
                        break;
                    case "Laser":
                        deadReason = "Laser";
                        break;
                    case "Bomb":
                        deadReason = "Bomb";
                        break;
                }

               Die();
            }
        }
        else 
        {
            switch (obj.tag)
            {
                case "Coin":
                    coinRound++;
                    break;
            }
        }
    }
    
}

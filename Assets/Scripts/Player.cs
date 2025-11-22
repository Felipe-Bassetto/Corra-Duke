using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Animation")]
    private Animator anim;

    [Header("GameObjects")]
    public GameObject Bala;
    public GameObject gameOverPanel;
    public Transform Jogador;
    public Transform posicaoSpawn;
    private Rigidbody2D rb;
    public LevelManager level;
    public GameDb db;
    public GameObject pauseMenu;
    public GameOverManager gameOver;
    
    [Header("Pulo")]
    public float distance = 2f;
    public float jumpForce;
    public float secondJump;
    public float extraJumpForce;
    public float maxJumpTime;
    public float jumpTimeCounter;
    public bool jumpUp = true;
    private bool jumpPressed, jumpHeld, jumpRelease;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;
    public enum PlayerState { Running, Jumping, DoubleJumping } 
    PlayerState state;
    public PlayerState currentState;
    bool pausePressed;
    
    [Header("Player")]
    public int maxHealth = 1;
    private int currentHealth;
    public string playerStatus = "Basic";
    public bool dead = false;
    public List<string> listPlayerVunerable = new List<string>();
    public string statusEspinho = "nothing";

    [Header("PowerUps")]
    private bool powerUpdActive = false;
    public float coinMagnetRadius = 5f;
    public float coinMagnetForce = 10f;
    public bool doubleScoreActive = false;
    public bool PowerUpdActive 
    {
        get { return powerUpdActive; }
        set { powerUpdActive = value; }
    }

    [Header("UI")]
    public int coinRound = 0;
    private int finalScore;
    public string deadReason;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1.8f;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        state = PlayerState.Running;
        ChangeState(state); // Garante animação inicial
    }

    void FixedUpdate()
    {
        // Nenhuma troca de animação aqui mais :)
    }

    void Update()
    {
        if (statusEspinho == "subindo") transform.Translate(Vector3.up * Time.deltaTime * 8f);
        else if (statusEspinho == "descendo") transform.Translate(Vector3.down * Time.deltaTime * 10f);

        if (!dead)
        {
            isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

            pausePressed = Input.GetKeyDown(KeyCode.Escape);
            jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
            jumpHeld = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W);
            jumpRelease = Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W);
        }
         

        if (pausePressed)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }

        if (jumpPressed && jumpUp)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                state = PlayerState.Jumping; 
            }
            else
            {
                jumpTimeCounter = 50f;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * secondJump, ForceMode2D.Impulse);
                jumpUp = false;
                state = PlayerState.DoubleJumping;
            }
        }
        
        if (isGrounded && jumpTimeCounter >= maxJumpTime)
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
                if (Input.GetMouseButtonDown(0))
                    Instantiate(Bala, posicaoSpawn.position, Quaternion.identity);
                break;

            case "Destroyer":
                Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position + Vector3.right * 5f, new Vector2(10f, 10f), 0f);
                foreach (Collider2D col in hits)
                    if (col.CompareTag("Bomb")) Destroy(col.gameObject);
                break;

            case "Shield":
                PowerUpdActive = true;
                break;

            case "CoinMagnet":
                rb.simulated = true;
                doubleScoreActive = true;
                Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, coinMagnetRadius);
                foreach (Collider2D coin in coins)
                {
                    if (coin.CompareTag("Coin"))
                    {
                        coin.transform.position = Vector2.MoveTowards(
                            coin.transform.position,
                            transform.position,
                            coinMagnetForce * Time.deltaTime
                        );
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
                anim.CrossFade(GetAnimationName("Running"), 0.1f);
                break;
            case PlayerState.Jumping:
                anim.CrossFade(GetAnimationName("Jumping"), 0.1f);
                break;
            case PlayerState.DoubleJumping:
                anim.CrossFade("Double Jump", 0.1f);
                break;
        }
    }

    string GetAnimationName(string baseAnim)
    {
        switch (playerStatus)
        {
            case "Basic":
            case "Destroyer":
                return $"Duke {baseAnim}";
            case "Gunner":
                return $"Gunner {baseAnim}";
            case "CoinMagnet":
                return $"Magnetic {baseAnim}";
            default:
                return $"Duke {baseAnim}";
        }
    }

    public void alterStatus(string newStatus)
    {
        playerStatus = newStatus;
        if (newStatus != "GoldRush")
            doubleScoreActive = false;
    }

    private void Die()
    {       
       gameOverPanel.SetActive(true);
       gameOver.ShowGameOver();

       finalScore = level.scoreMs;

       Configuracoes config = db.CarregarConfiguracoes();
       Progresso save = db.CarregarProgresso(config.Id);

       int record = save.ScoreRecord;
       int coins = save.Coins;

       if(record < finalScore) record = finalScore;  
       coins += coinRound;

       db.SalvarProgresso(config.Id, record, coins); 
    }

    void OnBecameInvisible()
    {
        deadReason = "fall";
        dead = true;
        Die();
    }

    IEnumerator morteBomba()
    {
        rb.simulated = false;
        anim.Play("Morte Bomba");
        yield return new WaitForSeconds(0.5f);
        Die();
    }

    IEnumerator morteLaser()
    {
        rb.simulated = false;
        anim.Play("Morte Laser");
        yield return new WaitForSeconds(0.5f);
        Die();
    }

    IEnumerator morteEspinho()
    {
        rb.simulated = false;
        anim.Play("Morte Espinho");
        statusEspinho = "subindo";
        yield return new WaitForSeconds(0.4f);
        statusEspinho = "descendo";
        yield return new WaitForSeconds(3f);
        Die();
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (listPlayerVunerable.Contains(obj.tag))
        {
            if (!PowerUpdActive)
            {
                switch (obj.tag)
                {
                    case "EnemyBullet":
                        deadReason = "enemy bullet";
                        dead = true;
                        break;
                    case "Laser":
                        deadReason = "laser";
                        dead = true;
                        StartCoroutine(morteLaser());
                        break;
                    case "Bomb":
                        deadReason = "bomb";
                        dead = true;
                        StartCoroutine(morteBomba());
                        break;
                    case "Spike":
                        deadReason = "spike";
                        dead = true;
                        StartCoroutine(morteEspinho());
                        break;
                    case "Nave":
                        deadReason = "Geese Ship";
                        dead = true;
                        StartCoroutine(morteEspinho());
                        break;
                }
            }
        }
        else 
        {
            if (obj.CompareTag("Coin"))
                coinRound++;
        }
    }
}

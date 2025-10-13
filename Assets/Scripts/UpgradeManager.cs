using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeItemController : MonoBehaviour
{
    [Header("Identidade do upgrade (exatamente como no DB)")]
    public string powerUpName = "CoinMagnet";

    [Header("Configuração de gameplay")]
    public int maxLevel = 7;                 // quantidade máxima de slots
    public int basePrice = 500;              // preço base
    public float duracaoAddPerLevel = 5f;    // quanto aumenta a duração por nível

    [Header("Referências UI")]
    public Image iconImage;                  // ícone do power-up
    public TextMeshProUGUI titleText;        // nome
    public Image[] slots;                    // array de imagens (cada slot é um quadradinho)
    public TextMeshProUGUI levelText;        // mostra "1/7", "2/7"
    public TextMeshProUGUI priceText;        // mostra preço ou "MAX"
    public Button upgradeButton;             // botão de comprar
    public TextMeshProUGUI playerCoinsText;  // mostra moedas do jogador (pra depois)

    [Header("Cores")]
    public Color filledColor = new Color(1f, 0.84f, 0f); // cor quando o slot está cheio
    public Color emptyColor = Color.white;               // cor quando está vazio

    // runtime
    private GameDb gameDb;
    private PowerUpsTable powerData;
    private Progresso progresso;
    private const int saveId = 1;

    void Awake()
    {
        // tenta localizar o GameDb na cena
        gameDb = FindObjectOfType<GameDb>();
        if (gameDb == null)
        {
            Debug.LogError("[UpgradeItemController] GameDb não encontrado na cena!");
            enabled = false;
            return;
        }
    }

    void OnEnable()
    {
        LoadFromDb();
        UpdateUI();

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(OnUpgradePressed);
        }
    }

    void OnDisable()
    {
        if (upgradeButton != null)
            upgradeButton.onClick.RemoveListener(OnUpgradePressed);
    }

    // Carrega os dados (powerup e progresso). Cria entradas se não existirem.
    public void LoadFromDb()
    {
        // carrega progresso (coins)
        progresso = gameDb.CarregarProgresso(saveId);
        if (progresso == null)
        {
            // cria progresso padrão e recarrega
            gameDb.CriarProgresso(1, 0);
            progresso = gameDb.CarregarProgresso(saveId);
        }

        // carrega dados do power up
        powerData = gameDb.CarregarPowerUps(saveId, powerUpName);
        if (powerData == null)
        {
            // cria entrada (nivel 0, duracao 5) e recarrega
            gameDb.CriarPowerUps(saveId, powerUpName, 0, 5f);
            powerData = gameDb.CarregarPowerUps(saveId, powerUpName);
        }
    }

    // Atualiza visual do card conforme dados carregados
    public void UpdateUI()
    {
        if (powerData == null || progresso == null) return;

        int nivel = powerData.Nivel;

        // pinta os slots
        if (slots != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < nivel) slots[i].color = filledColor;
                else slots[i].color = emptyColor;
            }
        }

        // texto de nível
        levelText.text = $"{nivel}/{maxLevel}";

        // preço / botao
        if (nivel >= maxLevel)
        {
            priceText.text = "MAX";
            upgradeButton.interactable = false;
        }
        else
        {
            int price = CalculatePrice(nivel);
            priceText.text = price.ToString();
            upgradeButton.interactable = (progresso.Coins >= price);
        }

        // mostra coins do jogador (p/ depois)
        if (playerCoinsText != null)
            playerCoinsText.text = progresso.Coins.ToString();

        // título
        if (titleText != null)
            titleText.text = powerUpName;
    }

    // Fórmula de preço (mudar o valor dos níveis)
    int CalculatePrice(int currentLevel)
    {
        return basePrice * (currentLevel + 1);
    }

    // Chamado quando jogador pressiona o botão de upgrade
    public void OnUpgradePressed()
    {
        // recarrega progresso atual (garantia)
        progresso = gameDb.CarregarProgresso(saveId);
        if (progresso == null) return;

        int nivel = powerData.Nivel;
        if (nivel >= maxLevel) return;

        int price = CalculatePrice(nivel);
        if (progresso.Coins < price)
        {
            // moedas insuficientes p/ feedback (se quiser colocar som depois)
            Debug.Log("[Upgrade] Moedas insuficientes");
            return;
        }

        // retira as moedas para compra e salva progresso
        progresso.Coins -= price;
        gameDb.SalvarProgresso(progresso.IdSave, progresso.ScoreRecord, progresso.Coins);

        // aumenta nivel e duração do power-up e salva
        nivel++;
        float novaDuracao = powerData.Duracao + duracaoAddPerLevel;
        gameDb.SalvarPowerUps(saveId, powerUpName, nivel, novaDuracao);

        // recarrega e atualiza UI
        LoadFromDb();
        RefreshAllCards(); // atualiza todos os cards para refletir coins e botões
    }

    // Atualiza os UpgradeItemController na cena 
    private void RefreshAllCards()
    {
        UpgradeItemController[] all = FindObjectsOfType<UpgradeItemController>();
        foreach (var c in all)
        {
            c.LoadFromDb();
            c.UpdateUI();
        }
    }
}


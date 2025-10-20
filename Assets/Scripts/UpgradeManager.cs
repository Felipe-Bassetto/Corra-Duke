using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Referências")]
    public GameDb gameDb;
    public TextMeshProUGUI nomePowerUpText;
    public TextMeshProUGUI custoText;
    public Image[] barrasDeNivel; 
    public Button botaoUpgrade;

    [Header("Configurações")]
    public string nomePowerUp;
    public int custoPorUpgrade = 500;
    public float duracaoPorNivel = 5f;
    public int nivelMaximo = 7;

    private PowerUpsTable powerUpData;
    private Progresso progresso;

    void Start()
    {
        // Carrega os dados do banco
        powerUpData = gameDb.CarregarPowerUps(1, nomePowerUp);
        progresso = gameDb.CarregarProgresso(1);

        AtualizarUI();

        botaoUpgrade.onClick.AddListener(TentarMelhorar);
    }

    void AtualizarUI()
    {
        // Atualiza a barra de progresso
        for (int i = 0; i < barrasDeNivel.Length; i++)
        {
            barrasDeNivel[i].color = i < powerUpData.Nivel ? Color.yellow : Color.gray;
        }
    }

    void TentarMelhorar()
    {
        if (powerUpData.Nivel >= nivelMaximo)
        {
            Debug.Log("Power-up já está no nível máximo!");
            return;
        }

        if (progresso.Coins < custoPorUpgrade)
        {
            Debug.Log("Moedas insuficientes!");
            return;
        }

        // Desconta moedas e melhora o power up
        progresso.Coins -= custoPorUpgrade;
        powerUpData.Nivel += 1;
        powerUpData.Duracao += duracaoPorNivel;

        // Salva 
        //gameDb.SalvarPowerUps(1, nomePowerUp, powerUpData.Nivel, powerUpData.Duracao);
        //gameDb.SalvarProgresso(1, progresso.ScoreRecord, progresso.Coins);

        // Atualiza a UI
        AtualizarUI();
    }
}

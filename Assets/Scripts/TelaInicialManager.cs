using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TelaInicialManager : MonoBehaviour
{
    [SerializeField] private string Game;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelSobre;
    [SerializeField] private GameObject nomeJogo;
    [SerializeField] private GameObject painelMelhorias;
    [SerializeField] private GameObject painelAlmanaque;
    [SerializeField] private AudioSource musicSource;  
    [SerializeField] private AudioClip musicTelaInicial;
    [SerializeField] private AudioClip musicUpgrades;
    [SerializeField] private AudioClip musicAlmanaque;
    [SerializeField] private GameDb mDb;
    [SerializeField] private TextMeshProUGUI record;
    [SerializeField] private TextMeshProUGUI shopCoins;
    [SerializeField] private GameController gameController;
    [SerializeField] private int recordValue;

    public string activePanel;


    private void Start()
    {
        Configuracoes config = mDb.CarregarConfiguracoes();

        int idNum = config.Id;

        Progresso progress = mDb.CarregarProgresso(idNum);

        recordValue = progress.ScoreRecord;

        record.text = "Record: " + recordValue;
        shopCoins.text = "$" + progress.Coins;

        gameController = FindFirstObjectByType<GameController>();

        switch(gameController.activePanel)
        {
            case "PanelUpgrade":
                AbrirMelhorias();
                break;
            case "PaneInstrucoes":
                AbrirAlmanaque();
                break;
        }
        
    }


    public void Jogar()
    {
        gameController.activatePanel(null);
        if (recordValue == 1)
        {
            SceneManager.LoadScene("Prologue");
        }
        else
        {
            SceneManager.LoadScene(Game);
        }
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
        nomeJogo.SetActive(false);
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
        nomeJogo.SetActive(true);
    }

    public void AbrirSobre()
    {
        painelMenuInicial.SetActive(false);
        painelSobre.SetActive(true);
        nomeJogo.SetActive(false);
    }

    public void FecharSobre()
    {
        painelSobre.SetActive(false);
        painelMenuInicial.SetActive(true);
        nomeJogo.SetActive(true);
    }

    public void AbrirMelhorias()
    {
        musicSource.clip = musicUpgrades;
        musicSource.Play();
        painelMenuInicial.SetActive(false);
        painelMelhorias.SetActive(true);
        nomeJogo.SetActive(false);
    }

    public void FecharMelhorias()
    {
        musicSource.clip = musicTelaInicial;
        musicSource.Play();
        painelMelhorias.SetActive(false);
        painelMenuInicial.SetActive(true);
        nomeJogo.SetActive(true);
    }

    public void AbrirAlmanaque()
    {
        musicSource.clip = musicAlmanaque;
        musicSource.Play();
        painelMenuInicial.SetActive(false);
        painelAlmanaque.SetActive(true);
        nomeJogo.SetActive(false);
    }

    public void FecharAlmanaque()
    {
        musicSource.clip = musicTelaInicial;
        musicSource.Play();
        painelAlmanaque.SetActive(false);
        painelMenuInicial.SetActive(true);
        nomeJogo.SetActive(true);
    }


    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }

}

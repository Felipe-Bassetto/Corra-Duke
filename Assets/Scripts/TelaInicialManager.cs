using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject painelInstrucoes;
    [SerializeField] private AudioSource musicSource;  
    [SerializeField] private AudioClip musicTelaInicial;
    [SerializeField] private AudioClip musicUpgrades;




    public void Jogar()
    {
        SceneManager.LoadScene(Game);
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

    public void AbrirInstrucoes()
    {
        painelInstrucoes.SetActive(true);
        painelMenuInicial.SetActive(false);
        nomeJogo.SetActive(false);
    }
    
    public void FecharInstrucoes()
    {
        painelInstrucoes.SetActive(false);
        painelMenuInicial.SetActive(true);
        nomeJogo.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }

}

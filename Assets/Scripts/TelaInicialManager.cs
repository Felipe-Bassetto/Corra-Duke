using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicialManager : MonoBehaviour
{
    [SerializeField] private string Game;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpçoes;
    [SerializeField] private GameObject nomeJogo;

    public void Jogar()
    {
        SceneManager.LoadScene(Game);
    }

    public void AbrirOpçoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpçoes.SetActive(true);
        nomeJogo.SetActive(false);
    }

    public void FecharOpçoes()
    {
        painelOpçoes.SetActive(false);
        painelMenuInicial.SetActive(true);
        nomeJogo.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}

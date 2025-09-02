using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UiManager : MonoBehaviour
{
    // Definição variáveis
    public int coins;
    public int score;

    // Definição de objetos UI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    // Definição game objects
    public Player player;
    public LevelManager scoreMs;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Moedas: " + player.coinRound;
        scoreText.text = "Pontuação: " + scoreMs.scoreMs;
    }
}

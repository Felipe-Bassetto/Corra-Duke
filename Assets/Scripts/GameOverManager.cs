using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; 

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Player player;
    [SerializeField] private LevelManager scoreMs;
    [SerializeField] private TextMeshProUGUI coinHud;
    [SerializeField] private TextMeshProUGUI scoreHud;
    [SerializeField] private TextMeshProUGUI deadHud;
    [SerializeField] private GameObject HudGame;
    public GameController gameController;
    private string newScene;
    public string deadBy;

    System.Random rnd = new System.Random();

    // Definição variáveis
    public int indexArray;

    [SerializeField] private RawImage postIt;
    public Texture[] arrPostIt;

    void Start()
    {
        gameOverPanel.SetActive(false); // começa invisível

        gameController = FindAnyObjectByType<GameController>();

        indexArray = rnd.Next(arrPostIt.Length);
        postIt.texture = arrPostIt[indexArray];
        
    }

    public void ShowGameOver()
    {
        Debug.Log("morreu");
        gameOverPanel.SetActive(true);
        //Time.timeScale = 0f; // pausa o jogo

        coinHud.text = "x" + player.coinRound;
        scoreHud.text = "" + scoreMs.scoreMs;
        deadHud.text = "" + player.deadReason;
        HudGame.SetActive(false);
    }

    public void Retry()
    {
        newScene = "Empty";
        gameController.activatePanel(newScene);
        //Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("MainPage"); // nome exato da cena do menu
    }

    public void GoToShop()
    {
        newScene = "PanelUpgrade";
        gameController.activatePanel(newScene);
        //Time.timeScale = 1f;
        SceneManager.LoadScene("MainPage");
    }

    public void GoToBook()
    {
        newScene = "PaneInstrucoes";
        gameController.activatePanel(newScene);
        //Time.timeScale = 1f;
        SceneManager.LoadScene("MainPage");
    }

}


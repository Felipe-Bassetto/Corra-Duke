using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Player player;
    [SerializeField] private LevelManager scoreMs;
    [SerializeField] private TextMeshProUGUI coinHud;
    [SerializeField] private TextMeshProUGUI scoreHud;
    private GameController gameController;
    private string newScene;

    void Start()
    {
        gameOverPanel.SetActive(false); // começa invisível

        gameController = FindAnyObjectByType<GameController>();
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // pausa o jogo

        coinHud.text = "x" + player.coinRound;
        scoreHud.text = "" + scoreMs.scoreMs;
    }

    public void Retry()
    {
        gameController.activatePanel(null);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainPage"); // nome exato da cena do menu
    }

    public void GoToShop()
    {
        newScene = "PanelUpgrade";
        gameController.activatePanel(newScene);
        gameController.test = newScene;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainPage");
    }

    public void GoToBook()
    {
        newScene = "PaneInstrucoes";
        gameController.activatePanel(newScene);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainPage");
    }

}


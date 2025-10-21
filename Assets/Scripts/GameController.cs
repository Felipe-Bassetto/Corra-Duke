using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public string activePanel;
    public static GameController instance;
    public GameObject painelUpgrade;
    public GameObject painelAlma;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void activatePanel(string namePanel)
    {
        activePanel = namePanel;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Só tentar ativar se houver um painel definido
        if (!string.IsNullOrEmpty(activePanel))
        {
            switch (activePanel)
            {
                case "PanelUpgrade":
                    painelUpgrade.SetActive(true);
                    break;
                case "PanelInstrucoes":
                    painelAlma.SetActive(true);
                    break;
            }

            // Limpa para evitar reabrir depois
            activePanel = "";
        }
    }
}

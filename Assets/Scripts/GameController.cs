using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public string activePanel;
    public static GameController instance;
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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void activatePanel(string namePanel)
    {
        activePanel = namePanel;
    }

    public string GetActivePanel()
    {
        return activePanel;
    }

}

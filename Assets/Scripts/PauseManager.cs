using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void continuePlaying()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainPage");
    }
}

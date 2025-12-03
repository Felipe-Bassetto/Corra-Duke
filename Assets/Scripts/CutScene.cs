using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class CutScene : MonoBehaviour
{

    public VideoPlayer vp;
    public GameObject message;
    public float countMs;
    private bool canSkip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp.Play();
        Debug.Log(vp.length);
    }

    // Update is called once per frame
    void Update()
    {

        if (countMs >= 0f)
        {
            countMs -= Time.deltaTime;
        }

        if (countMs <= 0f)
        {
            activeMessage();
        }


        if (Input.GetKeyDown(KeyCode.E) & canSkip)
        {
            if (SceneManager.GetActiveScene().name == "Cutscene") SceneManager.LoadScene("MainPage");
            else SceneManager.LoadScene("Game");
        }

        if (Input.anyKeyDown)
        {
            activeMessage();
        }

        Debug.Log(vp.time);
        if (vp.time >= vp.length - 0.5f)
        {
            Debug.Log("Cabo");
            StartCoroutine(GoToScene());
        }
    }

    IEnumerator GoToScene()
    {
        Debug.Log("cena");
        yield return new WaitForSeconds(1f);
        if (SceneManager.GetActiveScene().name == "Cutscene") SceneManager.LoadScene("MainPage");
        else SceneManager.LoadScene("Game");
    }

    void activeMessage()
    {
        message.SetActive(true);
        canSkip = true;
    }
}

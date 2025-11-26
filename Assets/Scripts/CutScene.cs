using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Video;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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
            SceneManager.LoadScene("MainPage");
        }

        if (Input.anyKeyDown)
        {
            activeMessage();
        }
    }

    void activeMessage()
    {
        message.SetActive(true);
        canSkip = true;
    }
}

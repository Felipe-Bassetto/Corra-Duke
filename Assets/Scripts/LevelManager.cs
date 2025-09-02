using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string worldName;
    public float countMs = 0f;
    public int scoreMs = 0;

    public Ground ground;

    public string velo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Soma o tempo desde o último frame em MILISSEGUNDOS

        countMs += Time.deltaTime * ground.velocidade; 

        // Enquanto tiver pelo menos 1 ms acumulado, dá pontos
        while (countMs >= 1f)
        {
            scoreMs++;
            countMs -= 1f;
        }
    }
}

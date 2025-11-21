using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Defini��o de vari�veis
    public string worldName;
    public float countMs = 0f;
    public int scoreMs = 0;
    public string velo;
    public bool groupActive = true;
    public int indexArr;
    public string listValue;
    public float counterTimePowerUp = 0f;
    public float timePowerUp;
    private float counterMult = 30f;
    public float counterSpawnPowerUp = 10f;
    private float counterVeloc = 9f;
    private bool canSpawnPowerUp = false;
    private int multScore = 1;

    // Defini��o de array
    public GameObject[] arrGroupsObs;

    // Defini��o de objetos
    System.Random rnd = new System.Random();
    public Ground soloGround;
    public GameObject powerUpPrefeb;
    public Player player;
    public int multPU; // Multiplicador do power up
    public string nomeConjunto;
    public float velocidade;
    public float timeSpawn;
    public GameObject nave;
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (counterTimePowerUp > 0f) 
        {
            counterTimePowerUp -= Time.deltaTime;
            
        }
        else 
        {
            player.alterStatus("Basic");
            player.PowerUpdActive = false;
        }

        // Soma o tempo desde o �ltimo frame em MILISSEGUNDOS
        countMs += Time.deltaTime * velocidade; 

        // Enquanto tiver pelo menos 1 ms acumulado, d� pontos
        while (countMs >= 1f)
        {
            if (player.doubleScoreActive)
                scoreMs += 2 * multScore; // dobra pontuação
            else
                scoreMs += 1 * multScore;
              
            countMs -= 1f;
        }

        // Contagem de tempo para aumentar o Multiplicador da pontuação
        if (counterMult > 0f)
        {
            counterMult -= Time.deltaTime;
        }
        else
        {
            counterMult = 30f;
            multScore++;
        }

        // Contagem de tempo para aumentar a velocidade
        if (counterVeloc > 0f)
        {
            counterVeloc -= Time.deltaTime;
        }
        else
        {
            counterVeloc = 9f;
            velocidade += 0.2f;
        }

        // Contagem de tempo para spawnar novo powerUp
        if (counterSpawnPowerUp > 0f)
        {
            counterSpawnPowerUp -= Time.deltaTime;
        }
        else
        {
            canSpawnPowerUp = true;
        }


        if (!groupActive)
        {
            spawnGroup();
        }

        if (timeSpawn > 0)
        {
            timeSpawn -= Time.deltaTime;
        }

    }

    public void SetGroupActive(bool isActive)
    {
        groupActive = isActive;
    }

    public void spawnGroup() // Randomiza e gera um conjunto de obstaculos aleat�rio
    {
        indexArr = rnd.Next(arrGroupsObs.Length);
        nomeConjunto = arrGroupsObs[indexArr].name;

        Debug.Log(indexArr);
        Debug.Log(nomeConjunto);

        switch (nomeConjunto)
        {
            case "Conjunto8": // Nave
                Instantiate(spawner, new Vector3(44, -4f, 0), Quaternion.identity);
                timeSpawn = 15f;
                break;

            case "Conjunto9": // Ganso
                Instantiate(spawner, new Vector3(44, -4f, 0), Quaternion.identity);
                timeSpawn = 15f;
                break;
        }

        Instantiate(arrGroupsObs[indexArr], new Vector3(22f,-4f,0), Quaternion.identity); // Gera o obstaculo
        Instantiate(soloGround, new Vector3(44f,-4f,0), Quaternion.identity); // Gera o ch�o vazio ap�s o conjunto

        if(canSpawnPowerUp)
        {
            Instantiate(powerUpPrefeb, new Vector3(44,-1f,0), Quaternion.identity);
            canSpawnPowerUp = false;
            counterSpawnPowerUp = 50f;
        }

        SetGroupActive(true);
    }

    public void SetPowerUpTime(float time)
    {
        timePowerUp = time;
        counterTimePowerUp = time;
    }

    
}

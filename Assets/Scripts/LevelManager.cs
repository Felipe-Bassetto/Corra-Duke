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

    // Defini��o de array
    public GameObject[] arrGroupsObs;

    // Defini��o de objetos
    System.Random rnd = new System.Random();
    public Ground soloGround;
    public Ground ground;
    public Player player;
    public int multPU; // Multiplicador do power up

    // Guarda posição original do chão
    private Vector3 groundOriginalPos;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        
        if (ground != null)
        {
            groundOriginalPos = ground.transform.position;

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
        countMs += Time.deltaTime * soloGround.velocidade; 
        Debug.Log(soloGround.velocidade);

        // Enquanto tiver pelo menos 1 ms acumulado, d� pontos
        while (countMs >= 1f)
        {
            scoreMs++;
            scoreMs = scoreMs * multPU;
            countMs -= 1f;
        }

        if (!groupActive)
        {
            spawnGroup();
        }
    }

    public void SetGroupActive(bool isActive)
    {
        groupActive = isActive;
    }

    public void spawnGroup() // Randomiza e gera um conjunto de obstaculos aleat�rio
    {
        indexArr = rnd.Next(arrGroupsObs.Length);
        Instantiate(arrGroupsObs[indexArr], new Vector3(22,-4.5f,0), Quaternion.identity); // Gera o obstaculo
        Instantiate(soloGround, new Vector3(44,-4.5f,0), Quaternion.identity); // Gera o ch�o vazio ap�s o conjunto

        SetGroupActive(true);
    }

    public void SetPowerUpTime(float time)
    {
        timePowerUp = time;
        counterTimePowerUp = time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Definição de variáveis
    public string worldName;
    public float countMs = 0f;
    public int scoreMs = 0;
    public Ground ground;
    public string velo;
    public bool groupActive = true;
    public int indexList;
    public string listValue;

    // Definição de array
    public GameObject[] arrGroupsObs;

    // Definição de objetos
    System.Random rnd = new System.Random();
    public Ground soloGround;


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

        if (!groupActive)
        {
            spawnGroup();
        }
    }

    public void SetGroupActive(bool isActive)
    {
        groupActive = isActive;
    }

    public void spawnGroup()
    {
        indexList = rnd.Next(arrGroupsObs.Length);
        Debug.Log(indexList);
        Debug.Log(arrGroupsObs[indexList]);
        Instantiate(arrGroupsObs[indexList], new Vector3(22,-4.5f,0), Quaternion.identity);
        Instantiate(soloGround, new Vector3(44,-4.5f,0), Quaternion.identity);

        SetGroupActive(true);
    }
}

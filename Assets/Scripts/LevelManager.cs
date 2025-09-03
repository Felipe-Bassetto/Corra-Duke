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
    public bool groupActive = false;
    public int indexList;
    public string listValue;

    // Definição de listas
    List<string> listGroupsObs = new List<string> {'Conjunto 1'};

    // Definição de objetos
    Random rnd = new Random();


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
            indexList = rnd.Next(listGroupsObs.Count);
            listValue = listGroupsObs[indexList];

            switch(listValue)
            {
                case "Conjunto 1":
                    break
            }

            groupActive = true;
        }
    }
}

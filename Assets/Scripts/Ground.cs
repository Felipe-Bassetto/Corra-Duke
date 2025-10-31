using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    // Definição das variáveis
    public GameObject soloGround;
    public GameObject cloneGround;

    // Definição de objetos
    public LevelManager level;
    
    // Start is called before the first frame update
    void Start()
    {
        if (level == null)
        {
            level = FindFirstObjectByType<LevelManager>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * level.velocidade * Time.deltaTime);
    }

    void OnBecameInvisible()
    {   
        if (gameObject.CompareTag("GroupGround")) // Verifica se é um conjunto. Caso seja, desativa o GroupActive para gerar um novo.
        {
            level.SetGroupActive(false);
        }
        else if (level.nomeConjunto == "Conjunto8" || level.nomeConjunto == "Conjunto9")
        {
            if (level.timeSpawn > 0)
            {
                Instantiate(cloneGround, new Vector3(22f, -4.5f, 0), Quaternion.identity);
            }
            else
            {
                level.nomeConjunto = "nothing";
                Instantiate(soloGround, new Vector3(22f, -4.5f, 0), Quaternion.identity);
                Instantiate(cloneGround, new Vector3(44, -4.5f, 0), Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}

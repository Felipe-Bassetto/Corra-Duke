using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    // Definição das variáveis
    float positionGroundx;


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

        positionGroundx = transform.position.x;
    }

    void OnBecameInvisible()
    {   
        if (gameObject.CompareTag("GroupGround")) // Verifica se é um conjunto. Caso seja, desativa o GroupActive para gerar um novo.
        {
            level.SetGroupActive(false);
        }

        Destroy(gameObject);
    }
}

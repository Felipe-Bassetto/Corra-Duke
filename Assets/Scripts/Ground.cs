using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    // Definição das variáveis
    public float velocidade = 8f;
    float positionGroundx;


    // Definição de objetos
    public LevelManager level;
    
    // Start is called before the first frame update
    void Start()
    {
        if (level == null)
        {
            level = FindObjectOfType<LevelManager>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);

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

    public void AlterarVelocidade(int multiplicador)
    {
        velocidade = velocidade * multiplicador;
    }
}

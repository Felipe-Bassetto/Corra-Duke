using UnityEngine;

public class TrapsGroups : MonoBehaviour
{

    // Definição de variáveis de outros objetos
    public Ground ground;

    //Definição de variáveis
    float positionGroupx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * ground.velocidade * Time.deltaTime);

        positionGroupx = transform.position.x;

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

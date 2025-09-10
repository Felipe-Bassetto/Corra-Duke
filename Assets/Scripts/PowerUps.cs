using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // Definição de arrays
    public string[] arrayPowerUps;

    // Definição de objetos
    System.Random rnd = new System.Random();
    public Player player;
    public Ground ground;

    // Definição variáveis
    public int indexArray;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * ground.velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D obj)
    {
        indexArray = rnd.Next(arrayPowerUps.Length);
        player.alterStatus(arrayPowerUps[indexArray]);
        Destroy(gameObject);
    }
}

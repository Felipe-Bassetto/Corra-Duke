using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // Definição de arrays
    public string[] arrayPowerUps;
    public float[] arrTimePowerUps;

    // Definição de objetos
    System.Random rnd = new System.Random();
    public Player player;
    public LevelManager levelManager;

    // Definição variáveis
    public int indexArray;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (levelManager == null)
        {
            levelManager = FindFirstObjectByType<LevelManager>();
        }
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * levelManager.velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D obj)
    {
        if(obj.tag == "Player")
        {
            indexArray = rnd.Next(arrayPowerUps.Length); // Pega o numero referente ao power up
            player.alterStatus(arrayPowerUps[indexArray]); // Altera o status do player para o power up
            levelManager.SetPowerUpTime(arrTimePowerUps[indexArray]); // Define a contagem de tempo no level manager
            Destroy(gameObject);
        }
    }
}

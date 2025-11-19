using UnityEngine;

public class Background : MonoBehaviour
{
    // Definição das variáveis
    public float velocidade;

    public SpriteRenderer spriteMain;
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        gameObject.GetComponent<SpriteRenderer>().color = spriteMain.color;

        if (!player.dead)
        {
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
            float positionBackgroundX = transform.position.x;

            if (positionBackgroundX <= -28f)
            {
                if (gameObject.tag == "Piramide") transform.position = new Vector3(28f, -1.5f, 0);
                else transform.position = new Vector3(28f, 0, 0);
            }
        }   
        
    }

}

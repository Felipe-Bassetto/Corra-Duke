using UnityEngine;

public class Background : MonoBehaviour
{
    // Definição das variáveis
    public float velocidade;

    public SpriteRenderer spriteMain;
    private Player player;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.dead)
        {
            gameObject.GetComponent<SpriteRenderer>().color = spriteMain.color;
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
            float positionBackgroundX = transform.position.x;

            if(positionBackgroundX <= -28f)
            {
                transform.position = new Vector3(28f, 0, 0);
            }
        }
        else
        {
            anim.enabled = false;
        }
        
    }

}

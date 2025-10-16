using UnityEngine;

public class Background : MonoBehaviour
{
    // Definição das variáveis
    public float velocidade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);
        float positionBackgroundX = transform.position.x;

        if(positionBackgroundX <= -28f)
        {
            transform.position = new Vector3(28f, 0, 0);
        }
    }

}

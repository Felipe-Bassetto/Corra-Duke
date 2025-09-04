using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    // Definição das variáveis
    public float velocidade = 5f;
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

        /*if(positionGroundx <= -22f)
        {
            transform.Translate(44f,0,0);
        }*/
    }

    void OnBecameInvisible()
    {   
        if (gameObject.CompareTag("GroupGround"))
        {
            level.SetGroupActive(false);
        }

        Destroy(gameObject);
    }
}

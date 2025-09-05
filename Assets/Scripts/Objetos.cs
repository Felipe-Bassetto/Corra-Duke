using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetos : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
    
    }

    //Configuração de colisão
    void OnTriggerEnter2D(Collider2D obj)
    {

        if(obj.CompareTag("Player"))
        {
           switch (gameObject.tag)
           {
               case "Bomb":
                   Destroy(gameObject);
                   break;
               case "Coin":
                   Destroy(gameObject);
                   break;
           }
        }
        else if(obj.CompareTag("PlayerBullet"))
        {
           switch (gameObject.tag)
           {
               case "Bomb":
                    Destroy(gameObject);
                    break;
           }
        }
    }
}
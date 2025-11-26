using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetos : MonoBehaviour
{
    [Header("Animation")]
    private Animator anim;

    [Header("Sound")]
    public GameObject music;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(music == null)
        {
            music = GameObject.Find("EffectsManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Spike" && gameObject.transform.position.x < -3.5f) anim.Play("Spike");
    }

    //Configuração de colisão
    void OnTriggerEnter2D(Collider2D obj)
    {
        SoundManager soundManagerScript = music.GetComponent<SoundManager>();
        if(obj.CompareTag("Player"))
        {
           switch (gameObject.tag)
           {
               case "Bomb":
                    soundManagerScript.SoundPlay(4);
                    anim.Play("Bomba");
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
                    soundManagerScript.SoundPlay(4);
                    Destroy(gameObject);
                    break;
           }
        }
    }

    public void Destroy() 
    {
        Destroy(gameObject);
    }
}
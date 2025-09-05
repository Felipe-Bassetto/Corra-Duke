using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedBullet = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Pega a posi��o do mouse em coordenadas de mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // garante que fica no mesmo plano 2D

        // Aponta em dire��o ao mouse
        LookAt2D(mousePos);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speedBullet * Time.deltaTime);
    }

    // Calcula dire��o da bala
    void LookAt2D(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Destroi objeto ao sair da tela
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.CompareTag("Bomb"))
        {
            Destroy(gameObject);
        }
    }
}



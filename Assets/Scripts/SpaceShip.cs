using UnityEngine;
using System.Collections;

public class SpaceShip : MonoBehaviour
{
    public float speed;
    public string state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "back")
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (gameObject.transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.transform.position.x > 5f)
        {
            StartCoroutine(voltar()); 
        } 
    }

    IEnumerator voltar()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 6;
        yield return new WaitForSeconds(2f);
        state = "front";
        speed = 15f;
    }
}

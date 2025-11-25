using UnityEngine;
using System.Collections;

public class SpaceShip : MonoBehaviour
{
    public float speed;
    public string state;
    private Collider2D col;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "back")
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        yield return new WaitForSeconds(2f);
        state = "front";
        speed = 15f;
        col.enabled = true;
    }
}

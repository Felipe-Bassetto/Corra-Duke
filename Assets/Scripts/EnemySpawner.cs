using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabNew;    
    public float spawnInterval = 3f;
    private float timer = 0f;
    private int countEnemy;
    public int quantSpawn;

    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            if(enemyPrefabNew == null)
            {
                Debug.LogError("enemyPrefabNew is null at spawn time");
                return;  // não deixa spawnar com prefab nulo (estava dando erro)
            }
            SpawnEnemy();
            countEnemy++;
            timer = 0f;
        }

        if (quantSpawn < countEnemy)
        {
            Destroy(gameObject);
        }
    }

    void SpawnEnemy()
    {
        float randomY = Random.Range(-2.5f, -0.5f);           
        Vector3 spawnPosition = new Vector3(10.5f, randomY, 0);

        Instantiate(enemyPrefabNew, spawnPosition, Quaternion.identity);
    }
}

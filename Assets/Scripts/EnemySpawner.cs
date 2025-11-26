using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabNew;
    public GameObject enemyShipNew;
    public LevelManager levelManager;
    private float spawnInterval;
    private float timer = 0f;
    private int countEnemy;
    public int quantSpawn;
    private string groupToSpawn;

    private void Start()
    {
        if (levelManager == null)
        {
            levelManager = FindFirstObjectByType<LevelManager>();
        }

        groupToSpawn = levelManager.nomeConjunto;

        if (groupToSpawn == "Conjunto9") spawnInterval = 3f;
        else if (groupToSpawn == "Conjunto8") spawnInterval = 1f;
    }

    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            if(groupToSpawn == "Conjunto9")
            {
                if (enemyPrefabNew == null)
                {
                    Debug.LogError("enemyPrefabNew is null at spawn time");
                    return;  // não deixa spawnar com prefab nulo (estava dando erro)
                }
                SpawnEnemy();
                countEnemy++;
                timer = 0f;
            }
            else if (groupToSpawn == "Conjunto8")
            {
                if (enemyShipNew == null)
                {
                    Debug.LogError("enemyShipNew is null at spawn time");
                    return;  // não deixa spawnar com prefab nulo (estava dando erro)
                }
                SpawnShip();
                countEnemy++;
                timer = 0f;
            }

        }

        if (quantSpawn < countEnemy)
        {
            Destroy(gameObject);
        }
    }

    void SpawnEnemy()
    {
        float randomY = Random.Range(-2.5f, 3f);           
        Vector3 spawnPosition = new Vector3(10.5f, randomY, 0);

        Instantiate(enemyPrefabNew, spawnPosition, Quaternion.identity);
    }

    void SpawnShip()
    {
        float randomY = Random.Range(-2.5f, -0.5f);
        Vector3 spawnPosition = new Vector3(-10.5f, randomY, 0);

        Instantiate(enemyShipNew, spawnPosition, Quaternion.identity);
    }
}

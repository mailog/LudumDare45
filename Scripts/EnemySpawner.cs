using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameManager gameManager;

    public Vector2[] spawnPoints;
    
    public float spawnCounter, spawnTime;

    public GameObject[] enemies;

    public int enemyCounter, enemyTotal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCounter < enemyTotal)
        {
            if (spawnCounter >= spawnTime)
            {
                enemyCounter++;
                Spawn();
                spawnCounter = 0;
            }
            else
            {
                spawnCounter += Time.deltaTime;
            }
        }
    }

    void Spawn()
    {
        Vector2 spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)];

        int chosenEnemy = Random.Range(0, enemies.Length);

        GameObject tmp = Instantiate(enemies[chosenEnemy], spawnPos, Quaternion.identity);
        tmp.GetComponent<EnemyReaction>().gameManager = gameManager;
    }
}

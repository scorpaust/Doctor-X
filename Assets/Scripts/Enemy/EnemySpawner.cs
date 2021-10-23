using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private Transform[] spawnPositions;

    [SerializeField]
    private int enemySpawnLimit = 6;

    [SerializeField]
    private float minSpawnWaitTime = 1f, maxSpawnWaitTime = 3f;

    private float spawnTimer;

    private GameObject newEnemy;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

	private void Awake()
	{
		if (instance == null)
		{
            instance = this;
		}
	}

	private void Update()
	{
        SpawnNewEnemy();
	}

    private void SpawnNewEnemy()
	{
        if (Time.time > spawnTimer)
		{
            if (spawnedEnemies.Count < enemySpawnLimit && GameManager.instance.GetGameState() != GameState.GAMEOVER)
			{
                newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPositions[Random.Range(0, spawnPositions.Length)].position, Quaternion.identity);

                spawnedEnemies.Add(newEnemy);

                ResetSpawnTimer();
			}
            else
			{
                ResetSpawnTimer();
			} 
                
		}
	}

    private void ResetSpawnTimer()
	{
        spawnTimer = Time.time + Random.Range(minSpawnWaitTime, maxSpawnWaitTime);
	}

    public void RemoveSpawnedEnemy(GameObject enemyToRemove)
	{
        spawnedEnemies.Remove(enemyToRemove);
	}
}

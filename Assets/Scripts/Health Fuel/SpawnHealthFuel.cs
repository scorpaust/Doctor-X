using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealthFuel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] healthFuels;

    [SerializeField]
    private int chanceToSpawn = 3;

	private Health enemyHealth;

	private void Awake()
	{
		enemyHealth = GetComponent<Health>();
	}

	private void OnDisable()
	{
		if (enemyHealth.GetEnemyHealth() != 0)
			return;

		if (Random.Range(0, 10) > chanceToSpawn)
		{
			Instantiate(healthFuels[Random.Range(0, healthFuels.Length)], transform.position, Quaternion.identity);
		}
	}
}

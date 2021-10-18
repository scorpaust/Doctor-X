using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int enemyHealth = 5;

    [SerializeField]
    private float playerMaxHealth = 100f;

    public float playerHealth;

	private void Start()
	{
        if (gameObject.CompareTag(TagManager.PLAYER_TAG))
		{

		}
	}

    public void EnemyTakeDamage(int amount)
	{
        enemyHealth -= amount;
	}

    public int GetEnemyHealth()
	{
        return enemyHealth;
	}

    public void PlayerTakeDamage(float amount)
	{
        playerHealth -= amount;
	}

    public float GetPlayerHealth()
	{
        return playerHealth;
	}

    public void IncreaseHealth(float amount)
	{
        playerHealth += amount;

        if (playerHealth > playerMaxHealth)
            playerHealth = playerMaxHealth;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
	[SerializeField]
	private EnemyBullet bullet_1, bullet_2;

	[SerializeField]
	private int initialPoolSize = 10;

	private List<EnemyBullet> enemyBullets = new List<EnemyBullet>();

	private void Awake()
	{
		InitializePool();
	}

	private void InitializePool()
	{
		GameObject newBullet;

		for (int i = 0; i < initialPoolSize; i++)
		{
			newBullet = Instantiate(bullet_1.gameObject);

			newBullet.transform.SetParent(transform);

			newBullet.SetActive(false);

			enemyBullets.Add(newBullet.GetComponent<EnemyBullet>());

			newBullet = Instantiate(bullet_2.gameObject);

			newBullet.transform.SetParent(transform);

			newBullet.SetActive(false);

			enemyBullets.Add(newBullet.GetComponent<EnemyBullet>());
		}
	}

	public void FireBullet(Vector3 bulletPos, bool setNegativeSpeed)
	{
		for (int i = 0; i < enemyBullets.Count; i++)
		{
			if (!enemyBullets[i].gameObject.activeInHierarchy)
			{
				enemyBullets[i].gameObject.SetActive(true);

				enemyBullets[i].gameObject.transform.position = bulletPos;

				if (setNegativeSpeed)
					enemyBullets[i].SetNegativeSpeed();

				return;
			}
		}
	}

	private void CreateNewBulletAndFire(Vector3 bulletPos, bool setNegativeSpeed)
	{
		GameObject newBullet;

		if (Random.Range(0, 2) > 0)
			newBullet = Instantiate(bullet_1.gameObject);
		else
			newBullet = Instantiate(bullet_2.gameObject);

		newBullet.transform.SetParent(transform);

		enemyBullets.Add(newBullet.GetComponent<EnemyBullet>());

		newBullet.transform.position = bulletPos;

		if (setNegativeSpeed)
		{
			newBullet.GetComponent<EnemyBullet>().SetNegativeSpeed();
		}
	}
}

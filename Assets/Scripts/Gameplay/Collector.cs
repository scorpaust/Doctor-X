using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagManager.PLAYER_BULLET_TAG))
			collision.gameObject.SetActive(false);

		if (collision.CompareTag(TagManager.ENEMY_BULLET_TAG))
			Destroy(collision.gameObject);
	}
}

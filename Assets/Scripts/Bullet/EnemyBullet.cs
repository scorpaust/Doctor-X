using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 15f;

    [SerializeField]
    private bool play_sound1;

    private Vector3 moveVector;

    private Vector3 tempScale;

	private TrailRenderer trail;

	private void Awake()
	{
		trail = transform.GetChild(0).GetComponent<TrailRenderer>();
	}

	private void Start()
	{
		if (play_sound1)
		{
			SoundManager.instance.Enemy_Weapon_1_Shoot();
		}
		else
		{
			SoundManager.instance.Enemy_Weapon_2_Shoot();
		}
	}

	private void Update()
	{
		MoveBullet();
	}

	private void OnDisable()
	{
		moveVector = Vector3.zero;

		moveSpeed = Mathf.Abs(moveSpeed);

		tempScale = transform.localScale;

		tempScale.x = Mathf.Abs(tempScale.x);

		transform.localScale = tempScale;

		trail.Clear();
	}

	private void MoveBullet()
	{
		moveVector.x = moveSpeed * Time.deltaTime;

		transform.position += moveVector;
	}

	public void SetNegativeSpeed()
	{
		moveSpeed = -Mathf.Abs(moveSpeed);

		tempScale = transform.localScale;

		tempScale.x = -Mathf.Abs(tempScale.x);

		transform.localScale = tempScale;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (gameObject.CompareTag(TagManager.ENEMY_BULLET_TAG) && collision.CompareTag(TagManager.PLAYER_TAG)) {

			gameObject.SetActive(false);
		}
	}
}

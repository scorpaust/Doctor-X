using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 15f;

	[SerializeField]
	private bool getTrailRenderer;

	private TrailRenderer trail;

    private Vector3 moveVector = Vector3.zero;

    private Vector3 tempScale;

	private void Awake()
	{
		if (getTrailRenderer)
			trail = transform.GetChild(0).GetComponent<TrailRenderer>();
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

		if (getTrailRenderer)
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
		if (collision.CompareTag(TagManager.ENEMY_TAG))
		{
			gameObject.SetActive(false);
		}
	}
}

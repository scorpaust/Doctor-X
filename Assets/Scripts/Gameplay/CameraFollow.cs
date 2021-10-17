using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float smoothSpeed = 2f;

    [SerializeField]
    private float playerBoundMinY = -1f, playerBoundMinX = -65f, playerBoundMaxX = 65f;

    [SerializeField]
    private float yGap = 2f;

    private Vector3 tempPos;

    private Transform playerTarget;

	private void Start()
	{
        playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
	}

	private void Update()
	{
        if (!playerTarget)
            return;

        tempPos = transform.position;

        if (playerTarget.position.y <= playerBoundMinY)
		{
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y, -10f), Time.deltaTime * smoothSpeed);
		}
        else
		{
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y + yGap, -10f), Time.deltaTime * smoothSpeed);
        }

        if (tempPos.x > playerBoundMaxX)
            tempPos.x = playerBoundMaxX;

        if (tempPos.x < playerBoundMinX)
            tempPos.x = playerBoundMinX;

        transform.position = tempPos;
	}
}

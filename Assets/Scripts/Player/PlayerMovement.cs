using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.5f;

    [SerializeField]
    private float minBoundX = -71f, maxBoundX = 71f, minBoundY = -3.3f, maxBoundY = 0f;

    private Vector3 tempPos;

    private float xAxis, yAxis;

    private PlayerAnimation playerAnimation;

	private void Awake()
	{
        playerAnimation = GetComponent<PlayerAnimation>();
	}

	private void Update()
	{
        HandleMovement();

        HandleAnimation();

        FlipSprite();
	}

    private void HandleMovement()
	{
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);

        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        tempPos = transform.position;

        tempPos.x += xAxis * moveSpeed * Time.deltaTime;

        tempPos.y += yAxis * moveSpeed * Time.deltaTime;

        if (tempPos.x < minBoundX)
            tempPos.x = minBoundX;

        if (tempPos.x > maxBoundX)
            tempPos.x = maxBoundX;

        if (tempPos.y < minBoundY)
            tempPos.y = minBoundY;

        if (tempPos.y > maxBoundY)
            tempPos.y = maxBoundY;

        transform.position = tempPos;
	}

    private void HandleAnimation()
	{
        if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
		{
            playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
		}
        else
		{
            playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
		}
	}

    private void FlipSprite()
	{
        if (xAxis > 0)
            playerAnimation.ChangeFacingDirection(true);
        else if (xAxis < 0)
            playerAnimation.ChangeFacingDirection(false);
	}


}

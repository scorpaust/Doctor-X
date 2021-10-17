using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.5f;

    [SerializeField]
    private float minBoundX = -71f, maxBoundX = 71f, minBoundY = -3.3f, maxBoundY = 0f;

    [SerializeField]
    private float shootWaitTime = 0.5f;

    [SerializeField]
    private float walkWaitTime = 0.3f;

    private float waitBeforeShooting;

    private float waitBeforeWalking;

    private bool canMove = true;

    private PlayerShootingManager playerShootingManager;

    private Vector3 tempPos;

    private float xAxis, yAxis;

    private PlayerAnimation playerAnimation;

	private void Awake()
	{
        playerAnimation = GetComponent<PlayerAnimation>();

        playerShootingManager = GetComponent<PlayerShootingManager>();
	}

	private void Update()
	{
        HandleMovement();

        HandleAnimation();

        FlipSprite();

        HandleShooting();

        CheckToMove();
	}

    private void HandleMovement()
	{
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);

        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        if (!canMove)
            return;

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

        if (!canMove)
            return;

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

    private void Shoot()
	{
        waitBeforeShooting = Time.time + shootWaitTime;

        StopMovement(walkWaitTime);

        playerAnimation.PlayAnimation(TagManager.SHOOT_ANIMATION_NAME);
	}

    private void StopMovement(float waitTime)
	{
        canMove = false;

        waitBeforeWalking = Time.time + waitTime;
	}

    private void HandleShooting()
	{
        /*if (Input.GetKeyDown(KeyCode.K))
		{
            if (Time.time > waitBeforeShooting)
			{
                Shoot();

                playerShootingManager.ShootBullet(transform.localScale.x);
			}
		} */
        
        if (playerShootingManager.GetWeaponType() == 1)
		{
            if (Input.GetKey(KeyCode.K))
			{
                Shoot();

                playerShootingManager.ShootElectricity(true);
			}
            else if (Input.GetKeyUp(KeyCode.K))
            {
                playerShootingManager.ShootElectricity(false);

                waitBeforeShooting = 0f;

                canMove = true;
            }
        }
        else
		{
            if (Input.GetKeyDown(KeyCode.K))
			{
                if (Time.time > waitBeforeShooting)
				{
                    Shoot();

                    playerShootingManager.ShootBullet(transform.localScale.x);
				}
			}
		}
        
	}

    private void CheckToMove()
	{
        if (Time.time > waitBeforeWalking)
		{
            canMove = true;
		}
	}

}

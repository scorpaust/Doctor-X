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

    [SerializeField]
    private Animator playerHurtFX;

    [SerializeField]
    private float damageColorWaitTime = 0.1f;

    private float damageColorTimer;

    private bool playerDamaged;

    private Color tempColor;

    private float waitBeforeShooting;

    private float waitBeforeWalking;

    private bool canMove = true;

    private PlayerShootingManager playerShootingManager;

    private Vector3 tempPos;

    private float xAxis, yAxis;

    private PlayerAnimation playerAnimation;

    private Health playerHealth;

    private bool hasDied;

    private SpriteRenderer sr;

	private void Awake()
	{
        playerAnimation = GetComponent<PlayerAnimation>();

        playerShootingManager = GetComponent<PlayerShootingManager>();

        playerHealth = GetComponent<Health>();

        sr = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{

        if (hasDied)
            return;

        HandleMovement();

        HandleAnimation();

        FlipSprite();

        HandleShooting();

        CheckToMove();

        ChangeDamageColor();
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

    private void RemovePlayerFromGame()
	{
        GameOverUIController.instance.GameOver();
        Destroy(gameObject);
	}

    private void PlayerReceivedDamage()
	{
        if (!playerDamaged)
		{
            tempColor = sr.material.color;

            tempColor.a = 1f;
            tempColor.r = 1f;
            tempColor.g = 0f;
            tempColor.b = 0f;

            sr.material.SetColor("_Color", tempColor);

            damageColorTimer = Time.time + damageColorWaitTime;

            playerDamaged = true;

            playerHurtFX.Play(TagManager.FX_ANIMATION_NAME);

            SoundManager.instance.PlayerHurt();
		}
	}

    private void ChangeDamageColor()
	{
        if (playerDamaged)
		{
            if (Time.time > damageColorTimer)
			{
                playerDamaged = false;

				tempColor = sr.material.color;

                tempColor.a = 1f;
                tempColor.r = 1f;
                tempColor.g = 1f;
                tempColor.b = 1f;

                sr.material.SetColor("_Color", tempColor);
			}
		}
	}

    public void TakeDamage(float amount)
	{
        if (hasDied)
            return;

        playerHealth.PlayerTakeDamage(amount);

        PlayerReceivedDamage();

        if (playerHealth.GetPlayerHealth() <= 0)
		{
            hasDied = true;

            playerAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);

            Invoke("RemovePlayerFromGame", 3f);
		}
        else
		{
            PlayerReceivedDamage();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagManager.ENEMY_BULLET_TAG))
		{
            TakeDamage(20f);
		}

        if (collision.CompareTag(TagManager.HEALTH_FUEL_TAG))
        {
            playerHealth.IncreaseHealth(collision.GetComponent<HealthFuel>().GetHealthValue());

            Destroy(collision.gameObject);
        }
    }
}

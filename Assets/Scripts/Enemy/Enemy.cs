using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Attack,
    Walk,
    Hit,
    Electric,
    Death
}

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float attackerStoppingDistance = 1.5f, shooterStoppingDistance = 8f;

    [SerializeField]
    private bool isShooter;

    [SerializeField]
    private float damageWaitTime = 0.5f;

    [SerializeField]
    private float attackWaitTime = 2.5f;

    [SerializeField]
    private float attackFinishWaitTime = 0.5f;

    [SerializeField]
    private EnemyDamageArea damageArea;

    [SerializeField]
    private EnemyBullet enemyBullet;

    [SerializeField]
    private Transform enemyBulletSpawnPos;

    private Transform playerTarget;

    private Vector3 tempScale;

    private float stoppingDistance;

    private PlayerAnimation enemyAnimation;

    private float damageTimer;

    private float attackTimer;

    private float attackFinishWaitTimer;

    private EnemyState enemyState;

    private EnemyBulletPool enemyBulletPool;

    private Health enemyHealth;

    private bool enemyDead;

	private void Awake()
	{
        if (GameManager.instance.GetGameState() != GameState.GAMEOVER)
		{
            playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;

            if (playerTarget && GameManager.instance.GetGameState() == GameState.GAMEPLAY)
            {
                playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;

                enemyAnimation = GetComponent<PlayerAnimation>();

                enemyBulletPool = GameObject.FindGameObjectWithTag(TagManager.ENEMY_POOL_TAG).GetComponent<EnemyBulletPool>();

                enemyHealth = GetComponent<Health>();
            }
        }
           
	}

	// Start is called before the first frame update
	void Start()
    {
        if (isShooter)
            stoppingDistance = shooterStoppingDistance;
        else
            stoppingDistance = attackerStoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDead)
            return;

        AnimateEnemy();

        SearchForPlayer();
    }

    private void AnimateEnemy()
	{
        if (playerTarget && GameManager.instance.GetGameState() == GameState.GAMEPLAY)
		{
            if (enemyState == EnemyState.Idle)
                enemyAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);

            if (enemyState == EnemyState.Attack)
                enemyAnimation.PlayAnimation(TagManager.ATTACK_ANIMATION_NAME);

            if (enemyState == EnemyState.Death)
                enemyAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);

            if (enemyState == EnemyState.Electric)
                enemyAnimation.PlayAnimation(TagManager.ELECTRIC_ANIMATION_NAME);

            if (enemyState == EnemyState.Hit)
                enemyAnimation.PlayAnimation(TagManager.HIT_ANIMATION_NAME);

            if (enemyState == EnemyState.Walk)
                enemyAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
        }
 
    }

    private void SearchForPlayer()
	{
        if (enemyState == EnemyState.Death)
            return;

        if (!playerTarget)
		{
            enemyState = EnemyState.Idle;

            return;
		}

        if (enemyState == EnemyState.Hit)
		{
            CheckIfDamageIsOver();
            return;
		}

        if (enemyState == EnemyState.Electric)
            return;

        if (Vector3.Distance(transform.position, playerTarget.position) > stoppingDistance)
		{
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);

            HandleFacingDirection();

            enemyState = EnemyState.Walk;
		}
        else
		{
            CheckIfAttackFinished();
            
            Attack();
		}
	}

    private void HandleFacingDirection()
	{        
        tempScale = transform.localScale;

        if (transform.position.x > playerTarget.position.x)
            tempScale.x = Mathf.Abs(tempScale.x);
        else
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;
	}

    private void CheckIfAttackFinished()
	{
        if (Time.time > attackFinishWaitTimer)
            enemyState = EnemyState.Idle;
	}

    private void Attack()
	{
        if (Time.time > attackTimer)
		{
            attackFinishWaitTimer = Time.time + attackFinishWaitTime;

            attackTimer = Time.time + attackWaitTime;

            enemyState = EnemyState.Attack;

            if (isShooter)
			{
                if (transform.position.x > playerTarget.transform.position.x)
				{
                    enemyBulletPool.FireBullet(enemyBulletSpawnPos.position, true);

				} else
				{
                    enemyBulletPool.FireBullet(enemyBulletSpawnPos.position, false);
                }
			}
		}
	}

    private void EnemyAttackerAttacked()
	{
        damageArea.gameObject.SetActive(true);

        damageArea.ResetDeactivateTimer();
	}

    private void EnemyDamaged(bool electricDamage)
	{
        if (electricDamage)
		{
            enemyState = EnemyState.Electric;

            DealDamage(2);
		}
        else
		{
            damageTimer = Time.time + damageWaitTime;

            enemyState = EnemyState.Hit;
            
            DealDamage(1);
        }
	}

    private void CheckIfDamageIsOver()
	{
        if (Time.time > damageTimer)
            enemyState = EnemyState.Idle;
	}

    private void DealDamage(int damageAmount)
	{
        enemyHealth.EnemyTakeDamage(damageAmount);

        if (enemyHealth.GetEnemyHealth() <= 0)
		{
            StartCoroutine(EnemyDied());
		}
	}

    private IEnumerator EnemyDied()
	{
        GetComponent<Collider2D>().enabled = false;

        enemyState = EnemyState.Death;

        yield return new WaitForEndOfFrame();

        enemyDead = true;

        yield return new WaitForSeconds(2f);

        RemoveEnemyFromGame();

        UIController.instance.SetKillScoreText();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagManager.PLAYER_BULLET_TAG))
		{
            EnemyDamaged(false);
		}

        if (collision.CompareTag(TagManager.ELECTRIC_BULLET_TAG))
		{
            EnemyDamaged(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.CompareTag(TagManager.ELECTRIC_BULLET_TAG))
        {
            enemyState = EnemyState.Idle;
        }
    }

    private void RemoveEnemyFromGame()
	{
        EnemySpawner.instance.RemoveSpawnedEnemy(gameObject);
        Destroy(gameObject);
	}
}

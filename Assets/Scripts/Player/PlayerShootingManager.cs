using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField]
    private Bullet[] bullets;

    [SerializeField]
    private Transform[] bulletSpawnPos;

    [SerializeField]
    private GameObject electricityBullet;

    private int weaponType;

    private BulletPool bulletPool;

    private Bullet currentBullet;

	private void Awake()
	{
        bulletPool = GetComponent<BulletPool>();

        InitializeBullets();
	}

    private void InitializeBullets()
	{
        for (int i = 0; i < bullets.Length; i++)
		{
            if (i == 1)
                continue;

            bulletPool.CreateBulletPool(i, bullets[i]);
		}
	}

    public void SetWeaponType(int newType)
	{
        weaponType = newType;
	}

    public int GetWeaponType()
	{
        return weaponType;
	}

    public void ShootBullet(float facingLeftSide)
	{
        currentBullet = bulletPool.GetBullet(weaponType);

        if (currentBullet != null)
		{
            currentBullet.gameObject.transform.position = bulletSpawnPos[weaponType].position;

            currentBullet.gameObject.SetActive(true);
        }
        else
		{
            currentBullet = Instantiate(bullets[weaponType]);

            currentBullet.gameObject.transform.position = bulletSpawnPos[weaponType].position;

            bulletPool.AddBulletToPool(weaponType, currentBullet);
        }

        if (facingLeftSide < 0)
            currentBullet.SetNegativeSpeed();
            
	}

    public void ShootElectricity(bool activateWeapon)
	{
        electricityBullet.SetActive(activateWeapon);
	}

}

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

    [SerializeField]
    private Animator bulletFX_1, bulletFX_2;

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

        if (weaponType == 0 || weaponType == 3)
		{
            bulletFX_1.gameObject.transform.position = bulletSpawnPos[weaponType].position;

            bulletFX_1.Play(TagManager.FX_ANIMATION_NAME);
        }
        else if (weaponType == 2)
		{
            bulletFX_2.gameObject.transform.position = bulletSpawnPos[weaponType].position;

            bulletFX_2.Play(TagManager.FX_ANIMATION_NAME);
        }
        
        if (weaponType == 0)
		{
            SoundManager.instance.Weapon_1_Shoot();
		}

        if (weaponType == 2)
        {
            SoundManager.instance.Weapon_3_Shoot();
        }

        if (weaponType == 3)
        {
            SoundManager.instance.Weapon_4_Shoot();
        }

    }

    public void ShootElectricity(bool activateWeapon)
	{
        electricityBullet.SetActive(activateWeapon);

        SoundManager.instance.Weapon_2_Shoot(activateWeapon);
	}

}

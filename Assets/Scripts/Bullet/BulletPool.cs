using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private List<Bullet> weapon_1_bulletPool = new List<Bullet>(), weapon_3_bulletPool = new List<Bullet>(), 
        weapon_4_bulletPool = new List<Bullet>();

    [SerializeField]
    private int initialBulletCount = 10;

    [SerializeField]
    private Transform bulletHolder;

    public void CreateBulletPool(int weaponType, Bullet bullet)
	{
        GameObject newBullet = null;

        if (weaponType == 0)
		{
            for (int i = 0; i < initialBulletCount; i++)
			{
                newBullet = Instantiate(bullet.gameObject);

                newBullet.SetActive(false);

                newBullet.transform.SetParent(bulletHolder);

                weapon_1_bulletPool.Add(newBullet.GetComponent<Bullet>());
			}
		}

        if (weaponType == 2)
        {
            for (int i = 0; i < initialBulletCount; i++)
            {
                newBullet = Instantiate(bullet.gameObject);

                newBullet.SetActive(false);

                newBullet.transform.SetParent(bulletHolder);

                weapon_3_bulletPool.Add(newBullet.GetComponent<Bullet>());
            }
        }

        if (weaponType == 3)
        {
            for (int i = 0; i < initialBulletCount; i++)
            {
                newBullet = Instantiate(bullet.gameObject);

                newBullet.SetActive(false);

                newBullet.transform.SetParent(bulletHolder);

                weapon_4_bulletPool.Add(newBullet.GetComponent<Bullet>());
            }
        }
    }
    
    public void AddBulletToPool(int weaponType, Bullet bullet)
	{
        if (weaponType == 0)
            weapon_1_bulletPool.Add(bullet);

        if (weaponType == 2)
            weapon_3_bulletPool.Add(bullet);

        if (weaponType == 3)
            weapon_4_bulletPool.Add(bullet);

        bullet.transform.SetParent(bulletHolder);
    }

    public Bullet GetBullet(int weaponType)
	{

        if (weaponType == 0)
		{
            for (int i = 0; i < weapon_1_bulletPool.Count; i++)
			{
                if (!weapon_1_bulletPool[i].gameObject.activeInHierarchy)
                    return weapon_1_bulletPool[i];
			}
		}

        if (weaponType == 2)
        {
            for (int i = 0; i < weapon_3_bulletPool.Count; i++)
            {
                if (!weapon_3_bulletPool[i].gameObject.activeInHierarchy)
                    return weapon_3_bulletPool[i];
            }
        }

        if (weaponType == 3)
        {
            for (int i = 0; i < weapon_4_bulletPool.Count; i++)
            {
                if (!weapon_4_bulletPool[i].gameObject.activeInHierarchy)
                    return weapon_4_bulletPool[i];
            }
        }

        return null;
	}
}

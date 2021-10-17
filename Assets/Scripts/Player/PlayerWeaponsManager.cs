using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    private PlayerAnimation playerAnim;

    private int weaponIndex;

    private int numberOfWeapons;

	private PlayerShootingManager playerShootingManager;

	private void Start()
	{
		playerAnim = GetComponent<PlayerAnimation>();

		playerShootingManager = GetComponent<PlayerShootingManager>();

		playerShootingManager.SetWeaponType(weaponIndex);

		numberOfWeapons = playerAnim.GetNumberOfWeapons();

		weaponIndex = 0;

		playerAnim.ChangeAnimatorController(weaponIndex);
	}

	private void Update()
	{
		ChangeWeapon();
	}

	private void ChangeWeapon()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			weaponIndex++;

			if (weaponIndex == numberOfWeapons)
				weaponIndex = 0;

			playerAnim.ChangeAnimatorController(weaponIndex);

			playerShootingManager.SetWeaponType(weaponIndex);

			Debug.Log("Weapon Type is: " + weaponIndex);
		}
	}
}

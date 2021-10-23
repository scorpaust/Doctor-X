using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

	[SerializeField]
	private AudioSource bgMainMenuMusic, bgGameplayMusic, weapon2_AudioSource;

	[SerializeField]
	private AudioClip weapon1_Audioclip, weapon3_Audioclip, weapon4_Audioclip;

	[SerializeField]
	private AudioClip[] playerHurt;

	[SerializeField]
	private AudioClip enemy_weapon_1_Audioclip, enemy_weapon_2_Audioclip;

	[SerializeField]
	private AudioClip clickSound;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;

			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void Weapon_1_Shoot()
	{
		AudioSource.PlayClipAtPoint(weapon1_Audioclip, transform.position);
	}

	public void Weapon_2_Shoot(bool playSound)
	{
		if (playSound)
		{
			if (!weapon2_AudioSource.isPlaying)
			{
				weapon2_AudioSource.Play();
			}
		}
		else
		{
			weapon2_AudioSource.Stop();
		}
	}

	public void Weapon_3_Shoot()
	{
		AudioSource.PlayClipAtPoint(weapon3_Audioclip, transform.position);
	}

	public void Weapon_4_Shoot()
	{
		AudioSource.PlayClipAtPoint(weapon4_Audioclip, transform.position);
	}

	public void PlayerHurt()
	{
		AudioSource.PlayClipAtPoint(playerHurt[Random.Range(0, playerHurt.Length)], transform.position);
	}

	public void Enemy_Weapon_1_Shoot()
	{
		AudioSource.PlayClipAtPoint(enemy_weapon_1_Audioclip, transform.position);
	}

	public void Enemy_Weapon_2_Shoot()
	{
		AudioSource.PlayClipAtPoint(enemy_weapon_2_Audioclip, transform.position);
	}

	public void PlayBGMusic(int sceneIndex)
	{
		if (sceneIndex == 0)
		{
			bgGameplayMusic.Stop();

			bgMainMenuMusic.Play();
		}		
		else
		{
			bgMainMenuMusic.Stop();

			bgGameplayMusic.Play();
		}
			
	}

	public void PlayClickSound()
	{
		AudioSource.PlayClipAtPoint(clickSound, transform.position);
	}
}

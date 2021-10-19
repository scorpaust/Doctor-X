using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField]
    private Slider playerHealthSlider;

    [SerializeField]
    private GameObject[] weaponIcons;

    [SerializeField]
    private Text killScoreTxt;

    private int killScoreCount;

    private int weaponIconIndex;

	private void Awake()
	{
        if (instance == null)
            instance = this;
	}

    public void InitializeHealthSlider(float minHealthValue, float maxHealthValue, float currentHealthValue)
	{
        playerHealthSlider.minValue = minHealthValue;

        playerHealthSlider.maxValue = maxHealthValue;

        playerHealthSlider.value = currentHealthValue;
	}

    public void SetHeathSliderValue(float newValue)
	{
        playerHealthSlider.value = newValue;
	}

    public void ChangeWeaponIcon()
	{
        weaponIcons[weaponIconIndex].SetActive(false);

        weaponIconIndex++;

        if (weaponIconIndex == weaponIcons.Length)
            weaponIconIndex = 0;

        weaponIcons[weaponIconIndex].SetActive(true);
    }

    public void SetKillScoreText()
	{
        killScoreCount++;

        killScoreTxt.text = "Kills: " + killScoreCount;
	}

    public int GetKillsCount()
	{
        return killScoreCount;
	}
}

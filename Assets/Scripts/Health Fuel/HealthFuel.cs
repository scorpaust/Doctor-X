using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFuel : MonoBehaviour
{
    [SerializeField]
    private float minDestroyTime = 2f, maxDestroyTime = 6f;

    [SerializeField]
    private float healthValue = 20f;

	private void Start()
	{
		Invoke("RemoveHealthFuelFromPlay", Random.Range(minDestroyTime, maxDestroyTime));
	}

	private void OnDisable()
	{
		CancelInvoke("RemoveHealthFuelFromPlay");
	}

	private void RemoveHealthFuelFromPlay()
	{
		Destroy(gameObject);
	}

	public float GetHealthValue()
	{
		return healthValue;
	}
}

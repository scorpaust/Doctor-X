using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Vector3 currentScale;

    private string currentAnim;

	[SerializeField]
	private RuntimeAnimatorController[] animControllers;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void PlayAnimation(string newAnimationToBePlayed)
	{

		if (currentAnim == newAnimationToBePlayed)
			return;

		anim.Play(newAnimationToBePlayed);

		currentAnim = newAnimationToBePlayed;
	}

	public void ChangeFacingDirection(bool faceRight)
	{
		currentScale = transform.localScale;

		if (faceRight)
		{
			currentScale.x = 1f;
		}
		else
		{
			currentScale.x = -1f;
		}

		transform.localScale = currentScale;
	}

	public void ChangeAnimatorController(int controllerIndex)
	{
		anim.runtimeAnimatorController = animControllers[controllerIndex];

		currentAnim = "";
	}

	public int GetNumberOfWeapons()
	{
		return animControllers.Length;
	}
}

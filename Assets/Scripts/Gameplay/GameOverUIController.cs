using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIController : MonoBehaviour
{
    public static GameOverUIController instance;

    [SerializeField]
    private Canvas gameOverCanvas;

    [SerializeField]
    private Text finalScoreTxt;

	[SerializeField]
	private Text highScoreTxt;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void GameOver()
	{
		GameManager.instance.SetGameState(GameState.GAMEOVER);

		MouseManager.instance.ShowMouse();

		gameOverCanvas.enabled = true;

		finalScoreTxt.text = "Score: " + UIController.instance.GetKillsCount();

		if (GameManager.instance.kills > GameManager.instance.highscore)
		{
			DataManager.instance.SavePlayerStats();

			highScoreTxt.text = "Highscore: " + GameManager.instance.highscore;
		}
		else
		{
			highScoreTxt.text = "Highscore: " + GameManager.instance.highscore;
		}
	}

	public void PlayAgain()
	{
		SoundManager.instance.PlayClickSound();

		GameManager.instance.SetGameState(GameState.GAMEPLAY);

		GameManager.instance.kills = 0;

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		SoundManager.instance.PlayBGMusic(1);
	}

	public void BackToMainMenu()
	{
		SoundManager.instance.PlayClickSound();

		GameManager.instance.SetGameState(GameState.MAINMENU);

		SceneManager.LoadScene(0);

		MouseManager.instance.ShowMouse();

		SoundManager.instance.PlayBGMusic(0);
	}
}

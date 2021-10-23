using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
	MAINMENU,
	GAMEPLAY,
	GAMEOVER
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	public string username;

	public int kills = 0, highscore = 0;

	private GameState gameState;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;

			DontDestroyOnLoad(gameObject);
		} else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		gameState = GameState.MAINMENU;
	}

	public void SetGameState(GameState state)
	{
		gameState = state;
	}

	public GameState GetGameState()
	{
		return gameState;
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;

	[SerializeField]
	private GameObject buttonsPanel, usernameInputPanel, scoreboardPanel;

	[SerializeField]
	private InputField usernameField;

	[SerializeField]
	private Text scoreboardFirstPlayerName, scoreboardSecondPlayerName, scoreboardThirdPlayerName;

	[SerializeField]
	private Text scoreboardFirstPlayerScore, scoreboardSecondPlayerScore, scoreboardThirdPlayerScore;

	public List<string> topPlayerNames = new List<string>();

	public List<int> topPlayerScores = new List<int>();

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		if (GameManager.instance.username == "")
			OpenInputUsernamePanel();

		DataManager.instance.LoadPlayerStats();
	}

	private void CloseAllPanels()
	{
		buttonsPanel.SetActive(false);

		usernameInputPanel.SetActive(false);

		scoreboardPanel.SetActive(false);
	}


	public void OpenInputUsernamePanel()
	{
		CloseAllPanels();

		usernameInputPanel.SetActive(true);

	}

	public void CloseInputUsernamePanel()
	{
		CloseAllPanels();

		GameManager.instance.username = usernameField.text;

		DataManager.instance.LoadPlayerStats();

		buttonsPanel.SetActive(true);
	}

	public void PlayGame()
	{
		GameManager.instance.SetGameState(GameState.GAMEPLAY);

		GameManager.instance.kills = 0;

		SceneManager.LoadScene(1);
	}

	public void ShowSortedScoreboard()
	{		
		UpdateScoreboard();

		CloseAllPanels();

		scoreboardPanel.SetActive(true);
	}


	public void UpdateScoreboard()
	{
		switch (DataManager.instance.playerStats.topPlayerNames.Count)
		{
			case 0:
				
				break;

			case 1:
				scoreboardFirstPlayerName.text = DataManager.instance.playerStats.topPlayerNames[0];

				scoreboardFirstPlayerScore.text = DataManager.instance.playerStats.topPlayerScores[0].ToString();

				break;
			case 2:
				scoreboardFirstPlayerName.text = DataManager.instance.playerStats.topPlayerNames[0];

				scoreboardFirstPlayerScore.text = DataManager.instance.playerStats.topPlayerScores[0].ToString();

				scoreboardSecondPlayerName.text = DataManager.instance.playerStats.topPlayerNames[1];

				scoreboardSecondPlayerScore.text = DataManager.instance.playerStats.topPlayerScores[1].ToString();

				break;
			case 3:
				scoreboardFirstPlayerName.text = DataManager.instance.playerStats.topPlayerNames[0];

				scoreboardFirstPlayerScore.text = DataManager.instance.playerStats.topPlayerScores[0].ToString();

				scoreboardSecondPlayerName.text = DataManager.instance.playerStats.topPlayerNames[1];

				scoreboardSecondPlayerScore.text = DataManager.instance.playerStats.topPlayerScores[1].ToString();

				scoreboardThirdPlayerName.text = DataManager.instance.playerStats.topPlayerNames[2];

				scoreboardThirdPlayerScore.text = DataManager.instance.playerStats.topPlayerScores[2].ToString();

				break;
			default:

				scoreboardFirstPlayerName.text = DataManager.instance.playerStats.topPlayerNames[0];

				scoreboardFirstPlayerScore.text = DataManager.instance.playerStats.topPlayerScores[0].ToString();

				scoreboardSecondPlayerName.text = DataManager.instance.playerStats.topPlayerNames[1];

				scoreboardSecondPlayerScore.text = DataManager.instance.playerStats.topPlayerScores[1].ToString();

				scoreboardThirdPlayerName.text = DataManager.instance.playerStats.topPlayerNames[2];

				scoreboardThirdPlayerScore.text = DataManager.instance.playerStats.topPlayerScores[2].ToString();
			
				return;
		}
		
	}

	public void BackToMainMenu()
	{
		CloseAllPanels();

		buttonsPanel.SetActive(true);
	}

	public void QuitGame()
	{
		DataManager.instance.SavePlayerStats();

		Application.Quit();
	}

}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public List<int> playerIndexes = new List<int>();

        public List<string> topPlayerNames = new List<string>();

        public List<int> topPlayerScores = new List<int>();
    }

    public static DataManager instance;

    public PlayerStats playerStats;

    private int indexCounter;

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

	private void Start()
	{
        if (GameManager.instance.username != null)
		{
			LoadPlayerStats();
		}

        indexCounter = playerStats.playerIndexes.Count;
	}

    private void IncrementIndex()
	{
        playerStats.playerIndexes.Add(indexCounter);

        indexCounter++;

        Debug.Log("Incremented");
	}

    private int ReturnIndexFromPlayerStats()
	{
        for (int i = 0; i < playerStats.topPlayerNames.Count; i++) {

            if (playerStats.topPlayerNames[i] == GameManager.instance.username)
			{
                return i;
			}
		}

        return -1;
	}

    private void SortTopPlayers()
	{
        for (int i = 0; i < playerStats.topPlayerNames.Count - 1; i++)
		{
            for (int j = 0; j < playerStats.topPlayerScores.Count - 1; j++)
			{
                if (playerStats.topPlayerScores[j+1] > playerStats.topPlayerScores[j])
				{
                    int temp = playerStats.topPlayerScores[j];
                    playerStats.topPlayerScores[j] = playerStats.topPlayerScores[j + 1];
                    playerStats.topPlayerScores[j + 1] = temp;

                    string temp2 = playerStats.topPlayerNames[j];
                    playerStats.topPlayerNames[j] = playerStats.topPlayerNames[j + 1];
                    playerStats.topPlayerNames[j + 1] = temp2;

                    int temp3 = playerStats.playerIndexes[j];
                    playerStats.playerIndexes[j] = playerStats.playerIndexes[j + 1];
                    playerStats.playerIndexes[j + 1] = temp3;
                }
            }
		}
	}

    public void SavePlayerStats()
    {
        if (GameManager.instance.kills > GameManager.instance.highscore)
        {
            IncrementIndex();

            playerStats.topPlayerNames.Add(GameManager.instance.username);

            playerStats.topPlayerScores.Add(GameManager.instance.kills);

            string json = JsonUtility.ToJson(playerStats);

            string path = Application.persistentDataPath + "/playerstats.json";

            File.WriteAllText(Application.persistentDataPath + "/playerstats.json", json);

        }
    }

    public void LoadPlayerStats()
    {
        string path = Application.persistentDataPath + "/playerstats.json";

        int index = ReturnIndexFromPlayerStats();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            playerStats = JsonUtility.FromJson<PlayerStats>(json);

            if (index != -1)
            {
                SortTopPlayers();

                GameManager.instance.username = playerStats.topPlayerNames[index];

                GameManager.instance.highscore = playerStats.topPlayerScores[index];

                MainMenuController.instance.UpdateScoreboard();

                return;
            }

            SortTopPlayers();

            MainMenuController.instance.UpdateScoreboard();
            
        }
    }
            

}

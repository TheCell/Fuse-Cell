using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTracker : MonoBehaviour
{
    private GameSettingsAccess gameSettings;
    private int playerCount = 0;
    private int[] pointsPerPlayer;
    private Tilelogic[] allTiles;

    private void Start()
    {
        gameSettings = Camera.main.GetComponent<GameSettingsAccess>();
        playerCount = gameSettings.GetAmountOfPlayers();

        allTiles = FindObjectsOfType<Tilelogic>();
        pointsPerPlayer = new int[playerCount];
    }

    public void CheckWinCondition()
    {
        pointsPerPlayer = new int[playerCount];
        bool allTilesHaveOwner = true;
        int winningPlayer = -1;
        int winningCount = -1;

        for (int i = 0; i < allTiles.Length; i++)
        {
            int playerWhoOwns = allTiles[i].GetPlayerOwner();
            if (playerWhoOwns > -1)
            {
                pointsPerPlayer[playerWhoOwns]++;
            }
            else
            {
                allTilesHaveOwner = false;
            }
        }

        for (int i = 0; i < pointsPerPlayer.Length; i++)
        {
            if (pointsPerPlayer[i] > winningCount)
            {
                winningPlayer = i;
                winningCount = pointsPerPlayer[i];
            }
            Debug.Log("player number " + i + " has " + pointsPerPlayer[i] + " points");
        }

        if (allTilesHaveOwner)
        {
            Debug.Log(winningPlayer + " player won!");
            RestartGame();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

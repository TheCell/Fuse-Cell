using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTracker : MonoBehaviour
{
    GameSettingsAccess gameSettings;
    int playerCount = 0;
    int[] pointsPerPlayer;
    Tilelogic[] allTiles;

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

        for (int i = 0; i < allTiles.Length; i++)
        {
            int playerWhoOwns = allTiles[i].GetPlayerOwner();
            if (playerWhoOwns > -1)
            {
                pointsPerPlayer[playerWhoOwns]++;
            }
        }

        for (int i = 0; i < pointsPerPlayer.Length; i++)
        {
            Debug.Log("player number " + i + " has " + pointsPerPlayer[i] + " points");
        }
    }
}

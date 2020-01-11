using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsAccess : MonoBehaviour
{
	[SerializeField] private GameSettingsScriptableObject gameSettings;

	private void Start()
	{
		if (gameSettings == null)
		{
			Debug.LogError("no gamesettings provided");
		}
	}

	public Color GetPlayerColor(int player)
	{
		Color playerColor = new Color(UnityEngine.Random.Range(0f, 1), UnityEngine.Random.Range(0f, 1), UnityEngine.Random.Range(0f, 1));
		if (player < gameSettings.playerColors.Length)
		{
			playerColor = gameSettings.playerColors[player];
		}

		return playerColor;
	}

	public int GetAmountOfPlayers()
	{
		return gameSettings.amountOfPlayers;
	}
}

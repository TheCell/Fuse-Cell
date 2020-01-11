using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettingsScriptableObject", order = 1)]
public class GameSettingsScriptableObject : ScriptableObject
{
	/*
	public bool debugMode;
	
	public float nearZPosition;
	public float middleZPosition;
	public float distantZPosition;[Header("Colors")]
	public Color buttonBgColor;
	public Color hudForegroundColor;// etc
	*/

	public Color[] playerColors;
}

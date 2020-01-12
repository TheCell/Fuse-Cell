﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilelogic : MonoBehaviour
{
	[Header("Player Colors")]
	public static Color[] playerColors;
    [Header("Top, Right, Bottom, Left, Close, Logo")]
    [SerializeField] private SpriteRenderer[] sprites;
	private ClosedTiles tileState = ClosedTiles.Open;
	private ClosedTiles previousState = ClosedTiles.Open;
	private int player;
	private GameSettingsAccess gameSettingsAccess;
	
	public void TileClicked(int player, Vector2 relativeClickPos, out bool tileClosed)
	{
		bool tileFinished = false;
		this.player = player;
		//tileState = Thecelleu.Utilities.RandomEnumValue<ClosedTiles>();
		ClosedTiles sideToSet = GetClickDirection(relativeClickPos);
		TileSet(sideToSet);
		if (tileState == ClosedTiles.Close)
		{
			Debug.Log("this tile closed");
			tileFinished = true;
		}
        UpdateDisplayedTexture();

		bool neighbourClosed = UpdatedNeighbour(sideToSet);
		if (neighbourClosed)
		{
			Debug.Log("neighbour tile closed");
			tileFinished = true;
		}

		tileClosed = tileFinished;
	}

	public bool TileUpdateFromNeighbour(int player, ClosedTiles externalTileDirection)
	{
		// externalTileDirection means the direction of the neighbour. Opposite of this tile side
		ClosedTiles sideToSet = ClosedTiles.Open;
		switch(externalTileDirection)
		{
			case ClosedTiles.Top:
				sideToSet = ClosedTiles.Bottom;
				break;
			case ClosedTiles.Right:
				sideToSet = ClosedTiles.Left;
				break;
			case ClosedTiles.Bottom:
				sideToSet = ClosedTiles.Top;
				break;
			case ClosedTiles.Left:
				sideToSet = ClosedTiles.Right;
				break;
		}

		this.player = player;
		TileSet(sideToSet);
		UpdateDisplayedTexture();

		return tileState == ClosedTiles.Close ? true : false;
	}

	/// <summary>
	/// returns the tile owner. Returns -1 if the tile is not closed yet.
	/// </summary>
	/// <returns>playernr owning the field</returns>
	public int GetPlayerOwner()
	{
		int fieldOwner = -1;
		if (tileState == ClosedTiles.Close)
		{
			fieldOwner = this.player;
		}

		return fieldOwner;
	}

	private void Start()
	{
		gameSettingsAccess = Camera.main.GetComponent<GameSettingsAccess>();
	}

	private void TileSet(ClosedTiles whichSideWasSet)
	{
		Thecelleu.FlagsHelper.Set(ref tileState, whichSideWasSet);
	}

	private void UpdateDisplayedTexture()
	{
		if (sprites == null)
		{
			Debug.LogError("no sprites Array");
			return;
		}
		
		if (Thecelleu.FlagsHelper.IsSet<ClosedTiles>(tileState, ClosedTiles.Open)
			&& !Thecelleu.FlagsHelper.IsSet<ClosedTiles>(previousState, ClosedTiles.Open))
		{
			for (int i = 0; i < sprites.Length; i++)
			{
				sprites[i].enabled = false;
			}
		}
		if (Thecelleu.FlagsHelper.IsSet<ClosedTiles>(tileState, ClosedTiles.Top)
			&& !Thecelleu.FlagsHelper.IsSet<ClosedTiles>(previousState, ClosedTiles.Top))
		{
			if (sprites.Length > 0)
			{
				sprites[0].color = GetPlayerColor();
				sprites[0].enabled = true;
			}
		}
		if (Thecelleu.FlagsHelper.IsSet<ClosedTiles>(tileState, ClosedTiles.Right)
			&& !Thecelleu.FlagsHelper.IsSet<ClosedTiles>(previousState, ClosedTiles.Right))
		{
			if (sprites.Length > 1)
			{
				sprites[1].color = GetPlayerColor();
				sprites[1].enabled = true;
			}
		}
		if (Thecelleu.FlagsHelper.IsSet<ClosedTiles>(tileState, ClosedTiles.Bottom)
			&& !Thecelleu.FlagsHelper.IsSet<ClosedTiles>(previousState, ClosedTiles.Bottom))
		{
			if (sprites.Length > 2)
			{
				sprites[2].color = GetPlayerColor();
				sprites[2].enabled = true;
			}
		}
		if (Thecelleu.FlagsHelper.IsSet<ClosedTiles>(tileState, ClosedTiles.Left)
			&& !Thecelleu.FlagsHelper.IsSet<ClosedTiles>(previousState, ClosedTiles.Left))
		{
			if (sprites.Length > 3)
			{
				sprites[3].color = GetPlayerColor();
				sprites[3].enabled = true;
			}
		}
		if (tileState == ClosedTiles.Close)
		{
			if (sprites.Length > 4)
			{
				// closed Walls
				sprites[4].color = GetPlayerColor();
				sprites[4].enabled = true;
			}
			if (sprites.Length > 5)
			{
				// logo
				sprites[5].color = GetPlayerColor();
				sprites[5].enabled = true;
			}
		}

		previousState = tileState;
	}

	private Color GetPlayerColor()
	{
		//PlayerSettings playerSettings = PlayerSettings.Instance;
		//Debug.Log(playerSettings.GetPlayerColors(this.player));
		//return playerSettings.GetPlayerColors(this.player);
		return gameSettingsAccess.GetPlayerColor(this.player);
	}

	private ClosedTiles GetClickDirection(Vector2 inputVector)
	{
		float signedAngle = Vector2.SignedAngle(new Vector2(1.0f, 0.0f), inputVector);
		ClosedTiles tilePart = ClosedTiles.Open;

		// cross straight
		// top
		if (signedAngle >= 45f && signedAngle < 135f)
		{
			tilePart = ClosedTiles.Top;
		}

		// left
		if (signedAngle > 135f || signedAngle <= -135f)
		{
			tilePart = ClosedTiles.Left;
		}

		// bottom
		if (signedAngle <= -45f && signedAngle > -135f)
		{
			tilePart = ClosedTiles.Bottom;
		}

		// right
		if (signedAngle >= -45f && signedAngle < 45f)
		{
			tilePart = ClosedTiles.Right;
		}

		return tilePart;
	}

	private bool UpdatedNeighbour(ClosedTiles activeSideOfCurrentTile)
	{
		bool neighbourClosed = false;
		Vector2 directionToCheck = Vector2.zero;

		switch(activeSideOfCurrentTile)
		{
			case ClosedTiles.Top:
				directionToCheck = Vector2.up;
				break;
			case ClosedTiles.Right:
				directionToCheck = Vector2.right;
				break;
			case ClosedTiles.Bottom:
				directionToCheck = Vector2.down;
				break;
			case ClosedTiles.Left:
				directionToCheck = Vector2.left;
				break;
		}

		int currentLayer = this.gameObject.layer; // move current object to ignore raycasts to avoid hitting itself
		this.gameObject.layer = 2;
		RaycastHit2D raycasthit = Physics2D.BoxCast(this.transform.position, new Vector2(0.8f, 0.8f), 0f, directionToCheck, 0.5f);
		Thecelleu.DebugExt.DrawBoxCast2D(this.transform.position, new Vector2(0.8f, 0.8f), 0f, directionToCheck, 0.5f, Color.white);
		this.gameObject.layer = currentLayer;

		if (raycasthit.collider != null)
		{
			Tilelogic otherTile = raycasthit.collider.GetComponent<Tilelogic>();
			neighbourClosed = otherTile.TileUpdateFromNeighbour(this.player, activeSideOfCurrentTile);
		}

		return neighbourClosed;
	}

    private void DebugEnum()
    {
        Debug.Log(ClosedTiles.Open);
        Debug.Log(ClosedTiles.Top);
        Debug.Log(ClosedTiles.Right);
        Debug.Log(ClosedTiles.Bottom);
        Debug.Log(ClosedTiles.Left);
        var a = (ClosedTiles)3;
        Debug.Log(a.ToString());
        Debug.Log(a == (ClosedTiles.Right | ClosedTiles.Top));
        Debug.Log(a == (ClosedTiles.Open | ClosedTiles.Right | ClosedTiles.Top));
        Debug.Log(a == ClosedTiles.Right);
        Debug.Log(ClosedTiles.Close);

        Debug.Log("ClosedTiles.Open is " + Thecelleu.FlagsHelper.IsSet(a, ClosedTiles.Open));
        Debug.Log("ClosedTiles.Top is " + Thecelleu.FlagsHelper.IsSet(a, ClosedTiles.Top));
        Debug.Log("ClosedTiles.Right is " + Thecelleu.FlagsHelper.IsSet(a, ClosedTiles.Right));
        Debug.Log("ClosedTiles.Bottom is " + Thecelleu.FlagsHelper.IsSet(a, ClosedTiles.Bottom));
        Debug.Log("ClosedTiles.Left is " + Thecelleu.FlagsHelper.IsSet(a, ClosedTiles.Left));
    }
}

[Flags]
public enum ClosedTiles
{
    Open = 0b_0000_0000,
    Top = 0b_0000_0001,
    Right = 0b_0000_0010,
    Bottom = 0b_0000_0100,
    Left = 0b_0000_1000,
    Close = Top | Right | Bottom | Left // or operator to set bits to 1
}
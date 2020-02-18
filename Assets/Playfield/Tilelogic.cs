using System;
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
	
	// and this is what we call lazy ass dirty multipurpose function. I just want this game shipped :)
	public bool TileClicked(int player, Vector2 relativeClickPos, out bool somethingChanged)
	{
		bool tileFinished = false;
		//tileState = Thecelleu.Utilities.RandomEnumValue<ClosedTiles>();
		ClosedTiles beforeclick = tileState;
		ClosedTiles sideToSet = GetClickDirection(relativeClickPos);

		if (tileState == ClosedTiles.Close)
		{
			Debug.Log("this tile closed");
			tileFinished = true;
		}
		else
		{
			this.player = player;
			TileSet(sideToSet);
			if (tileState == ClosedTiles.Close)
			{
				tileFinished = true;
			}
		}
		UpdateDisplayedTexture();

		if (beforeclick == tileState)
		{
			somethingChanged = false;
		}
		else
		{
			somethingChanged = true;
		}

		bool neighbourClosed = UpdatedNeighbour(sideToSet);
		if (neighbourClosed)
		{
			//Debug.Log("neighbour tile closed");
			tileFinished = true;
		}

		return tileFinished;
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

		if (tileState == ClosedTiles.Close)
		{
			Debug.Log("Tile already closed");
		}
		else
		{
			Debug.Log("updating neighbour for player " + player);
			this.player = player;
			TileSet(sideToSet);
		}
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

	public void CheckForNeighboursAndSetupWalls()
	{
		RaycastHit2D hitTop = GetNeighbourForDirection(Vector2.up);
		RaycastHit2D hitRight = GetNeighbourForDirection(Vector2.right);
		RaycastHit2D hitBottom = GetNeighbourForDirection(Vector2.down);
		RaycastHit2D hitLeft = GetNeighbourForDirection(Vector2.left);

		if (hitTop.collider == null)
		{
			TileSet(ClosedTiles.Top);
		}

		if (hitRight.collider == null)
		{
			TileSet(ClosedTiles.Right);
		}

		if (hitBottom.collider == null)
		{
			TileSet(ClosedTiles.Bottom);
		}

		if (hitLeft.collider == null)
		{
			TileSet(ClosedTiles.Left);
		}

		UpdateDisplayedTexture(true);
	}

	public void ResetTile()
	{
		tileState = ClosedTiles.Open;
		previousState = ClosedTiles.Open;
		player = 0;

		if (sprites == null)
		{
			Debug.LogError("no sprites Array");
			return;
		}
		for (int i = 0; i < sprites.Length; i++)
		{
			sprites[i].enabled = false;
		}
		UpdateDisplayedTexture(true);
	}

	private void Start()
	{
		gameSettingsAccess = Camera.main.GetComponent<GameSettingsAccess>();
		UpdateDisplayedTexture(true);
	}

	private void TileSet(ClosedTiles whichSideWasSet)
	{
		Thecelleu.FlagsHelper.Set(ref tileState, whichSideWasSet);
	}

	private void UpdateDisplayedTexture(bool isNeutral = false)
	{
		if (sprites == null)
		{
			Debug.LogError("no sprites Array");
			return;
		}

		Color playerColor = Color.white;
		if (!isNeutral)
		{
			 playerColor = GetPlayerColor();
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
				sprites[0].color = playerColor;
				sprites[0].enabled = true;
			}
		}
		if (Thecelleu.FlagsHelper.IsSet<ClosedTiles>(tileState, ClosedTiles.Right)
			&& !Thecelleu.FlagsHelper.IsSet<ClosedTiles>(previousState, ClosedTiles.Right))
		{
			if (sprites.Length > 1)
			{
				sprites[1].color = playerColor;
				sprites[1].enabled = true;
			}
		}
		if (Thecelleu.FlagsHelper.IsSet<ClosedTiles>(tileState, ClosedTiles.Bottom)
			&& !Thecelleu.FlagsHelper.IsSet<ClosedTiles>(previousState, ClosedTiles.Bottom))
		{
			if (sprites.Length > 2)
			{
				sprites[2].color = playerColor;
				sprites[2].enabled = true;
			}
		}
		if (Thecelleu.FlagsHelper.IsSet<ClosedTiles>(tileState, ClosedTiles.Left)
			&& !Thecelleu.FlagsHelper.IsSet<ClosedTiles>(previousState, ClosedTiles.Left))
		{
			if (sprites.Length > 3)
			{
				sprites[3].color = playerColor;
				sprites[3].enabled = true;
			}
		}
		if (tileState == ClosedTiles.Close)
		{
			if (sprites.Length > 4)
			{
				// closed Walls
				sprites[4].color = playerColor;
				sprites[4].enabled = true;
			}
			if (sprites.Length > 5)
			{
				// logo
				sprites[5].color = playerColor;
				sprites[5].enabled = true;
			}
		}

		previousState = tileState;
		//Debug.Log(tileState);
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

		RaycastHit2D neighbourHit = GetNeighbourForDirection(directionToCheck);
		Thecelleu.DebugExt.DrawBoxCast2D(this.transform.position, new Vector2(0.8f, 0.8f), 0f, directionToCheck, 0.5f, Color.white);

		if (neighbourHit.collider != null)
		{
			Tilelogic otherTile = neighbourHit.collider.GetComponent<Tilelogic>();
			neighbourClosed = otherTile.TileUpdateFromNeighbour(this.player, activeSideOfCurrentTile);
		}

		return neighbourClosed;
	}

	private RaycastHit2D GetNeighbourForDirection(Vector2 directionToCheck)
	{
		int currentLayer = this.gameObject.layer; // move current object to ignore raycasts to avoid hitting itself
		this.gameObject.layer = 2;
		RaycastHit2D raycasthit = Physics2D.BoxCast(this.transform.position, new Vector2(0.8f, 0.8f), 0f, directionToCheck, 0.5f); // magic numbers: 0.8 because I assume tiles are 1 unit in size and 0.5 I just want to make sure to hit the next withouth going too far
		this.gameObject.layer = currentLayer;
		return raycasthit;
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
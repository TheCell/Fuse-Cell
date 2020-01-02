using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilelogic : MonoBehaviour
{
    [Header("Open, Top, Right, Bottom, Left, Close")]
    [SerializeField] private Sprite[] sprites;
	private ClosedTiles tileState = ClosedTiles.Open;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

	public void TileClicked()
	{
		Debug.Log("I was clicked");
        tileState = Thecelleu.Utilities.RandomEnumValue<ClosedTiles>();
        UpdateDisplayedTexture();
    }
	
	private void TileSet(ClosedTiles whichSideWasSet)
	{
		Thecelleu.FlagsHelper.Set(ref tileState, whichSideWasSet);
	}

	private void UpdateDisplayedTexture()
	{
        if (spriteRenderer != null)
        {
            switch(tileState)
            {
                case ClosedTiles.Open:
                    if (sprites.Length > 0)
                    {
                        spriteRenderer.sprite = sprites[0];
                    }
                    break;
                case ClosedTiles.Top:
                    if (sprites.Length > 1)
                    {
                        spriteRenderer.sprite = sprites[1];
                    }
                    break;
                case ClosedTiles.Right:
                    if (sprites.Length > 2)
                    {
                        spriteRenderer.sprite = sprites[2];
                    }
                    break;
                case ClosedTiles.Bottom:
                    if (sprites.Length > 3)
                    {
                        spriteRenderer.sprite = sprites[3];
                    }
                    break;
                case ClosedTiles.Left:
                    if (sprites.Length > 4)
                    {
                        spriteRenderer.sprite = sprites[4];
                    }
                    break;
                case ClosedTiles.Close:
                    if (sprites.Length > 5)
                    {
                        spriteRenderer.sprite = sprites[5];
                    }
                    break;
                default:
                    break;
            }
        }
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

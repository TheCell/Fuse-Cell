using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilelogic : MonoBehaviour
{

    private void Start()
    {
        
    }

    private void Update()
    {
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBorders : MonoBehaviour
{
    public void UpdateWalls()
    {
        Tilelogic[] allTiles = FindObjectsOfType<Tilelogic>();
        
        for (int i = 0; i < allTiles.Length; i++)
        {
            allTiles[i].CheckForNeighboursAndSetupWalls();
        }

        Debug.Log("Walls for " + allTiles.Length + " Tiles updated");
    }

    public void ResetTiles()
    {
        Tilelogic[] allTiles = FindObjectsOfType<Tilelogic>();

        for (int i = 0; i < allTiles.Length; i++)
        {
            allTiles[i].ResetTile();
        }
    }
    private void Start()
    {
        UpdateWalls();
    }
}

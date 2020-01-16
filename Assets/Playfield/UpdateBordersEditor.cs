using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UpdateBorders))]
public class UpdateBordersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        UpdateBorders updateBorders = (UpdateBorders)target;

        if (GUILayout.Button("Update Walls"))
        {
            updateBorders.UpdateWalls();
        }

        if (GUILayout.Button("Reset Tiles"))
        {
            updateBorders.ResetTiles();
        }
    }
}

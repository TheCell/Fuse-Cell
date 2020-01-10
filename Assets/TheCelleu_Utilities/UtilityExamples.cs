using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityExamples : MonoBehaviour
{
    [SerializeField] private GameObject dampLerpCubeZero;
    [SerializeField] private GameObject dampLerpCubeAtoB;
    private bool dampLerpReverse = false;
    private float dampMinimalZ;
    private float dampMaximalZ;

    private void Start()
    {
        dampMinimalZ = dampLerpCubeAtoB.transform.position.z;
        dampMaximalZ = dampMaximalZ + 5f;

        BitSettingExample();
        ShowDifferentEnums();
        CheckPointSide();
        CreateListFromArrayAndCopyIndices();
    }

    private void Update()
    {
        DampExample();
    }

    private void DampExample()
    {
        // from A to Zero
        Vector3 posToZero = dampLerpCubeZero.transform.position;
        posToZero.z = Thecelleu.Utilities.Damp(posToZero.z, 0.3f, Time.deltaTime);
        dampLerpCubeZero.transform.position = posToZero;

        // from A to B
        Vector3 pos = dampLerpCubeAtoB.transform.position;
        if (dampLerpReverse)
        {
            pos.z = Thecelleu.Utilities.Damp(pos.z, dampMinimalZ, 0.7f, Time.deltaTime);
        }
        else
        {
            pos.z = Thecelleu.Utilities.Damp(pos.z, dampMaximalZ, 0.3f, Time.deltaTime);
        }
        dampLerpCubeAtoB.transform.position = pos;

        if (pos.z >= dampMaximalZ - 0.1f) // because it is theoretically never gonna hit the dampMaximalZ
        {
            dampLerpReverse = !dampLerpReverse;
        }
        else if (pos.z <= dampMinimalZ + 0.1f) // because it is theoretically never gonna hit the dampMinimalZ
        {
            dampLerpReverse = !dampLerpReverse;
        }
    }

    private void BitSettingExample()
    {
        Debug.Log("BitSettingExample");
        BitSettingExampleNames names = BitSettingExampleNames.Susan | BitSettingExampleNames.Bob;

        bool susanIsIncluded = Thecelleu.FlagsHelper.IsSet(names, BitSettingExampleNames.Susan);
        bool karenIsIncluded = Thecelleu.FlagsHelper.IsSet(names, BitSettingExampleNames.Karen);

        Debug.Log("is susan included in " + names + ": " + susanIsIncluded);
        Debug.Log("is karen included in " + names + ": " + karenIsIncluded);

		Thecelleu.FlagsHelper.Set(ref names, BitSettingExampleNames.Karen);
		Debug.Log("is karen included in " + names + " now?: " + karenIsIncluded);

		Thecelleu.FlagsHelper.Unset(ref names, BitSettingExampleNames.Karen);
		Debug.Log("is karen included in " + names + " now?: " + karenIsIncluded);
	}

    private void ShowDifferentEnums()
    {
        Debug.Log("ShowDifferentEnums");
        string valuesAsString = "";

        for (int i = 0; i < 5; i++)
        {
            var value = Thecelleu.Utilities.RandomEnumValue<System.DayOfWeek>();
            valuesAsString += " " + value;
        }
        
        Debug.Log(valuesAsString.ToString());
    }

    private void CheckPointSide()
    {
        Debug.Log("CheckPointSide");
        Vector3 planePointA = new Vector3(1, 0, 0);
        Vector3 planePointB = new Vector3(0, 1, 0);
        Vector3 planePointC = new Vector3(0, 0, 1);

        Debug.Log("Point  2  2  2 is on the right side of the plane: " + Thecelleu.Utilities.GetSide(planePointA, planePointB, planePointC, new Vector3(2, 2, 2)));
        Debug.Log("Point -2 -2 -2 is on the right side of the plane: " + Thecelleu.Utilities.GetSide(planePointA, planePointB, planePointC, new Vector3(-2, -2, -2)));
    }

    private void CreateListFromArrayAndCopyIndices()
    {
        Debug.Log("CreateListFromArrayAndCopyIndices");
        int[] integers = new int[] { 4, 12, 42 };
        List<int> integersAsList = Thecelleu.Utilities.CreateList<int>(integers);
        Debug.Log(integersAsList[0] + " " + integersAsList[1] + " " + integersAsList[2]);

        List<int> integersToCopyElementsFrom = new List<int> { 101, 102, 103 };
        Debug.Log(integersToCopyElementsFrom.Count + " " + integersToCopyElementsFrom[2]);
        Thecelleu.Utilities.Copy<int>(ref integersAsList, integersToCopyElementsFrom, 2);
        Debug.Log(integersAsList[0] + " " + integersAsList[1] + " " + integersAsList[2] + " " + integersAsList[3]);
    }
}

[Flags]
public enum BitSettingExampleNames
{
    None = 0,
    Susan = 1,
    Bob = 2,
    Karen = 4
}
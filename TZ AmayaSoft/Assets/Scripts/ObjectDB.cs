using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDB : MonoBehaviour
{
    public static SortedDictionary<int, CellObject> DB = new SortedDictionary<int, CellObject>();

    void Awake()
    {
        foreach (CellObject cellObject in Resources.LoadAll<CellObject>("ScriptableObjects/Objects"))
        {
            if (!DB.ContainsKey(cellObject.id))
            {
                DB.Add(cellObject.id, cellObject);
            }

            //Debug.Log(cellObject.id);
        }
    }
}

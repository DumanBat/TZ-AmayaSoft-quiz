using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsDB : MonoBehaviour
{
    public static SortedDictionary<int, LevelParameter> DB = new SortedDictionary<int, LevelParameter>();

    void Awake()
    {
        foreach (LevelParameter levelParameter in Resources.LoadAll<LevelParameter>("ScriptableObjects/LevelParameters"))
        {
            if (!DB.ContainsKey(levelParameter.id))
            {
                DB.Add(levelParameter.id, levelParameter);
            }

            //Debug.Log(levelParameter.id);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class ObjectSpawner : MonoBehaviour
{
    public GameObject cellObject;

    [Header("Grid attributes")]
    public int x_Space;
    public int y_Space;

    public float x_StartPos;
    public float y_StartPos;

    public List<int> usedObjectsId = new List<int>();
    public List<int> usedCorrectObject = new List<int>();

    public void spawnObjects()
    {
        int columnLength = gameObject.GetComponent<LevelManager>().columnLength;
        int currentDifficulty = gameObject.GetComponent<LevelManager>().currentDifficulty;

        int idMin = gameObject.GetComponent<LevelManager>().idMin;
        int idMax = gameObject.GetComponent<LevelManager>().idMax;

        int correctAnswerNumber = gameObject.GetComponent<LevelManager>().correctAnswerNumber;



        for (int i = 0; i < columnLength * currentDifficulty; i++)
        {
            Vector3 position = new Vector3(x_StartPos + (x_Space * (i % columnLength)), y_StartPos + (-y_Space * (i / columnLength)));

            int idRandom = Random.Range(idMin, idMax + 1);
            idRandom = checkRandomInt(idRandom, idMin, idMax, false);

            GameObject _object;

            if (i != correctAnswerNumber)
            {
                usedObjectsId.Add(idRandom);

                _object = Instantiate(cellObject, position, Quaternion.identity);
                _object.GetComponent<ObjectBehaviour>().cellObject = ObjectDB.DB[idRandom];
            }
            else
            {
                idRandom = checkRandomInt(idRandom, idMin, idMax, true);
                usedObjectsId.Add(idRandom);
                usedCorrectObject.Add(idRandom);

                _object = Instantiate(cellObject, position, Quaternion.identity);
                _object.GetComponent<ObjectBehaviour>().cellObject = ObjectDB.DB[idRandom];
                _object.GetComponent<ObjectBehaviour>().isCorrectAnswer = true;

                gameObject.GetComponent<LevelManager>()._correctAnswerObject = _object;
            }

            if (i == (columnLength * currentDifficulty) - 1) usedObjectsId.Clear();

            if (usedCorrectObject.Count >= (idMax - idMin))
            {
                usedCorrectObject.Clear();
                Debug.Log("List of used correct objects cleared. StackOverflow Exception NO-NO-NO, krasno - ploho");
            }
        }
    }

    int checkRandomInt(int idRandom, int idMin, int idMax, bool isCorrectAnswerCheck)
    {
        if (!isCorrectAnswerCheck)
        {
            if (!usedObjectsId.Contains(idRandom))
            {
                return idRandom;
            }
            else
            {
                return checkRandomInt(Random.Range(idMin, idMax + 1), idMin, idMax, false);
            }
        }
        else
        {
            if (!usedCorrectObject.Contains(idRandom) && !usedObjectsId.Contains(idRandom))
            {
                return idRandom;
            }
            else
            {
                return checkRandomInt(Random.Range(idMin, idMax +1), idMin, idMax, true);
            }
        }
        
    }

}

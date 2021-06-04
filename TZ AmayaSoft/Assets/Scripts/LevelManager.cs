using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Game Parameters")]
    //public int maxRowCount;
    public int levelTypeId;

    public int levelsInDifficulty;
    public int currentLevelInDifficulty;
    public int currentDifficulty;

    [Header("Level Parameters")]
    
    public int columnLength;
    public int rowLength;

    public int idMin; 
    public int idMax;

    public int correctAnswerNumber;

    [Header("UI elements")]
    public GameObject restartButton;
    public GameObject blankPanel;

    public GameObject _goalDisplayText;
    TextMeshProUGUI goalDisplayText;
    public float textFadeDuration;

    public Image fadeImage;
    public float fadeDuration;

    public AudioSource wrongAnswerSound;
    public AudioSource correctAnswerSound;

    [HideInInspector]
    public GameObject _correctAnswerObject;
    public UnityEvent correctAnswerEvent;
    public UnityEvent disableButtonsEvent;


    private void Awake()
    {
        _goalDisplayText.SetActive(false);

        goalDisplayText = _goalDisplayText.GetComponent<TextMeshProUGUI>();

        restartButton.SetActive(false);

        goalDisplayText.canvasRenderer.SetAlpha(0f);

    }
    private void Start()
    {
        doFade(1);
        setLevelParameters(levelTypeId);
        Invoke("startGame", fadeDuration);
        
        if (correctAnswerEvent == null) correctAnswerEvent = new UnityEvent();

        correctAnswerEvent.AddListener(changeLevel);
        disableButtonsEvent.AddListener(disableButtons);
    }
    public void startGame()
    {
        blankPanel.SetActive(false);

        if(fadeImage.canvasRenderer.GetAlpha() == 1) doFade(1);

        currentDifficulty = 1;
        currentLevelInDifficulty = 1;

        correctAnswerNumber = Random.Range(0, (columnLength * currentDifficulty));

        gameObject.GetComponent<ObjectSpawner>().spawnObjects();

        displayGoal();
    }

    void changeLevel()
    {
        if (currentDifficulty == 3 && currentLevelInDifficulty == levelsInDifficulty)
        {
            doFade(0);
            restartButton.SetActive(true);
        }
        else
        {
            if (currentLevelInDifficulty == levelsInDifficulty)
            {
                currentDifficulty += 1;
                currentLevelInDifficulty = 1;
            }
            else
            {
                currentLevelInDifficulty += 1;
            }

            blankPanel.SetActive(false);

            correctAnswerNumber = Random.Range(0, (columnLength * currentDifficulty));

            destroyCells();

            gameObject.GetComponent<ObjectSpawner>().spawnObjects();

            displayGoal();
        }
    }

    void displayGoal()
    {
        _goalDisplayText.SetActive(true);
        goalDisplayText.text = $"Find {_correctAnswerObject.GetComponent<ObjectBehaviour>().cellObject.objectName}";

        doFadeText(0);
    }

    public void restartGame()
    {
        blankPanel.SetActive(false);

        _goalDisplayText.SetActive(false);
        restartButton.SetActive(false);

        doFade(1);
        doFadeText(goalDisplayText.canvasRenderer.GetAlpha());

        destroyCells();

        Invoke("startGame", fadeDuration);
    }

    void destroyCells()
    {
        GameObject[] cells;
        cells = GameObject.FindGameObjectsWithTag("cell");
        foreach (GameObject cell in cells)
        {
            Destroy(cell);
        }
    }

    void doFade(float alpha)
    {
        fadeImage.CrossFadeAlpha(System.Math.Abs(alpha - 1), fadeDuration, false);
    }

    void doFadeText(float alpha)
    {
        goalDisplayText.CrossFadeAlpha(System.Math.Abs(alpha - 1), textFadeDuration, false);
    }

    void disableButtons()
    {
        blankPanel.SetActive(true);
    }

    void setLevelParameters(int levelTypeId)
    {
        levelsInDifficulty = LevelsDB.DB[levelTypeId].levelsInDifficulty;

        idMin = LevelsDB.DB[levelTypeId].objectsMinId;
        idMax = LevelsDB.DB[levelTypeId].objectsMaxId;
    }
}

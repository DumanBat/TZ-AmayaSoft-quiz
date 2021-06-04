using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ObjectBehaviour : MonoBehaviour, IPointerDownHandler
{
    public LevelManager levelManager;

    public CellObject cellObject;
    SpriteRenderer _cellObjectSprite;
    public bool isCorrectAnswer = false;

    Transform cellTransform;
    public Ease cellAnimEase;
    public float cellAnimationDuration;

    Transform cellObjectTransform;
    public Ease objectAnimEase;

    ParticleSystem particleSystem;
    public Ease correctObjectAnimEase;
    public float correctObjectAnimationDuration;

    void Awake()
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();

    }
    private void Start()
    {
        _cellObjectSprite = transform.Find("objectSprite").GetComponent<SpriteRenderer>();
        _cellObjectSprite.sprite = cellObject.objectSprite;

        particleSystem = transform.Find("StarParticleSystem").GetComponent<ParticleSystem>();

        if (levelManager.currentDifficulty == 1 && levelManager.currentLevelInDifficulty == 1) animateCell();

        cellObjectTransform = transform.Find("objectSprite").transform;
    }


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!isCorrectAnswer)
        {
            levelManager.wrongAnswerSound.Play();
            animateWrongObject();
        }
        else
        {
            levelManager.correctAnswerSound.Play();
            levelManager.disableButtonsEvent.Invoke();
            animateCorrectObject();
            particleSystem.Play();
            Invoke("delayCorrectAnswerEvent", 2f);
        }
    }

    void animateCell()
    {
        cellTransform = gameObject.transform;
        cellTransform
            .DOScale(1, cellAnimationDuration)
            .SetEase(cellAnimEase)
            .From(0)
            ;
    }

    void animateWrongObject()
    {
        cellObjectTransform
            .DOKill(true)
            ;

        cellObjectTransform            
            .DOShakePosition(0.5f, new Vector3(0.15f, 0.15f, 0), 10, 45)
            .SetEase(objectAnimEase)
            ;
    }

    void animateCorrectObject()
    {
        cellObjectTransform
            .DOScale(0.55f, correctObjectAnimationDuration)
            .SetEase(correctObjectAnimEase)
            .From(0.45f)
            ;
    }

    void delayCorrectAnswerEvent()
    {
        levelManager.correctAnswerEvent.Invoke();
    }
}

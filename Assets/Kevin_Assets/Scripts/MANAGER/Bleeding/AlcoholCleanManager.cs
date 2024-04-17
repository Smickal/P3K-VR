using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class AlcoholCleanManager : MonoBehaviour
{
    public static AlcoholCleanManager Instance;

    [SerializeField] float _timeToCleanGrabber = 3f;
    [Header("Reference")]
    [SerializeField]private GameManager gameManager;
    [SerializeField]private BleedingWithoutEmbeddedItem bleedingWithoutEmbeddedItem;

    [Header("Reference - Hand")]
    [SerializeField] Grabber _leftGrabber;
    [SerializeField] Grabber _rightGrabber;

    [HideInInspector] public bool IsHolding = false;

    bool isLeftCleaned = false;
    bool isRightCleaned = false;
    bool isDoneCleaning = false;
    public bool IsDoneCleaning{get{return isDoneCleaning;}}

    float currLeftTime;
    float currRightTime;

    float currTime;
    Grabber currentGrabber;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
        

    }

    private void Update()
    {
        if (!IsHolding || gameManager.GameStateNow() != GameState.InGame || gameManager.LevelTypeNow() != LevelP3KType.Bleeding || gameManager.InGame_ModeNow() != InGame_Mode.FirstAid || bleedingWithoutEmbeddedItem.StateNow() != BleedingWithoutEmbeddedItem_State.WearGloves) return;

        currTime += Time.deltaTime;

        if (currentGrabber == _leftGrabber && currTime + currLeftTime > _timeToCleanGrabber)
        {
            isLeftCleaned = true;
            currTime = 0;
            IsHolding = false;

            CheckHandsAllClean();
        }

        else if (currentGrabber == _rightGrabber && currTime + currRightTime > _timeToCleanGrabber)
        {
            isRightCleaned = true;
            currTime = 0;
            IsHolding = false;

            CheckHandsAllClean();
            
        }
    }

    public void RegisterGrabber(Grabber grabber)
    {
        currentGrabber = grabber;
    }


    public void SaveCurrentTimeProgress()
    {

        if (currentGrabber == _leftGrabber)
        {
            currLeftTime += currTime;
        }

        else if (currentGrabber == _rightGrabber)
        {
            currRightTime += currTime;
        }

        currentGrabber = null;
        currTime = 0f;
    }

    public void CheckHandsAllClean()
    {
        if(isDoneCleaning)return;
        if((isLeftCleaned && !isRightCleaned) || (!isLeftCleaned && isRightCleaned))
        {
            Debug.Log("Bersihkan tangan 1 nya");
        }
        else if (isLeftCleaned && isRightCleaned)
        {
            isDoneCleaning = true;
            Debug.Log("All Done");
        }
    }
}

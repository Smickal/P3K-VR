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
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BleedingWithoutEmbeddedItem bleedingWithoutEmbeddedItem;
    [SerializeField] private ParticleSystem _whiteSparkleParticle;
    [SerializeField] private ParticleSystem _yellowSparkleParticle;

    [Header("Reference - HandController")]
    [SerializeField] GameObject _leftGrabber;
    [SerializeField] GameObject _rightGrabber;

    [Header("Reference - HandTrack")]
    [SerializeField] GameObject _leftGrabberHT;
    [SerializeField] GameObject _rightGrabberHT;


    [HideInInspector] public bool IsHolding = false;

    bool isLeftCleaned = false;
    bool isRightCleaned = false;
    bool isDoneCleaning = false;
    public bool IsDoneCleaning{get{return isDoneCleaning;}}

    float currLeftTime;
    float currRightTime;

    float currTime;
    GameObject currentGrabber;
    GameObject currentNeedToCleanGrabber, lastNeedToCleanGrabber;

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
        // if (!IsHolding || gameManager.GameStateNow() != GameState.InGame || gameManager.LevelTypeNow() != LevelP3KType.Bleeding || gameManager.InGame_ModeNow() != InGame_Mode.FirstAid || bleedingWithoutEmbeddedItem.StateNow() != BleedingWithoutEmbeddedItem_State.CleanHands) return;
        if(!IsHolding)return;

        if (currentGrabber == null || currentNeedToCleanGrabber == null) return;

        currTime += Time.deltaTime;

        if ((currentNeedToCleanGrabber == _leftGrabber || currentNeedToCleanGrabber == _leftGrabberHT) && currTime + currLeftTime > _timeToCleanGrabber)
        {
            isLeftCleaned = true;
            currTime = 0;
            IsHolding = false;

            CheckHandsAllClean();
        }

        else if ((currentNeedToCleanGrabber == _rightGrabber || currentNeedToCleanGrabber == _rightGrabberHT) && currTime + currRightTime > _timeToCleanGrabber)
        {
            isRightCleaned = true;
            currTime = 0;
            IsHolding = false;

            CheckHandsAllClean();
            
        }
    }

    public void RegisterGrabber(GameObject grabber)
    {
        currentGrabber = grabber;     
    }

    public void RegisterCurrentNeedToCleanGrabber(GameObject grabber)
    {
        if (IsHolding == false || grabber == currentGrabber) return;
        currentNeedToCleanGrabber = grabber;

        _whiteSparkleParticle.Play();
        _whiteSparkleParticle.transform.SetParent(grabber.transform);
        _whiteSparkleParticle.transform.localPosition = Vector3.zero;

    }

    public void UnRegisterCurrentNeedToCleanGrabber(GameObject grabber)
    {
        if (IsHolding == false || grabber == currentGrabber || currentNeedToCleanGrabber == null)return;
        if(currentNeedToCleanGrabber == grabber)
        {
            lastNeedToCleanGrabber = grabber;
            currentNeedToCleanGrabber = null;
        }
    }

    public void SaveCurrentTimeProgress()
    {
        if(currentNeedToCleanGrabber != null)
        {
            if (currentNeedToCleanGrabber == _leftGrabber || currentNeedToCleanGrabber == _leftGrabberHT)
            {
                currLeftTime += currTime;
            }

            else if (currentNeedToCleanGrabber == _rightGrabber || currentNeedToCleanGrabber == _rightGrabberHT)
            {
                currRightTime += currTime;
            }
        }
        else
        {
            if (lastNeedToCleanGrabber== _leftGrabber || lastNeedToCleanGrabber == _leftGrabberHT)
            {
                currLeftTime += currTime;
            }

            else if (lastNeedToCleanGrabber == _rightGrabber || lastNeedToCleanGrabber == _rightGrabberHT)
            {
                currRightTime += currTime;
            }
        }
        
        lastNeedToCleanGrabber = null;
        currentNeedToCleanGrabber = null;
        currentGrabber = null;
        currTime = 0f;

        _whiteSparkleParticle.Stop();
        _whiteSparkleParticle.transform.SetParent(this.transform);
        _whiteSparkleParticle.transform.localPosition = Vector3.zero;
    }

    public void CheckHandsAllClean()
    {
        if(isDoneCleaning)return;

        if((isLeftCleaned && !isRightCleaned) || (!isLeftCleaned && isRightCleaned))
        {
            Debug.Log("A Hand is Cleared!");

            _yellowSparkleParticle.Stop();
            _yellowSparkleParticle.transform.position = currentGrabber.transform.position;
            _yellowSparkleParticle.Play();
            
        }

        else if (isLeftCleaned && isRightCleaned)
        {
            isDoneCleaning = true;
            Debug.Log("All Done");
        }
    }
    public void Reset()
    {
        isLeftCleaned = false;
        isRightCleaned = false;
        isDoneCleaning = false;
        currRightTime = 0f;
        currLeftTime = 0f;

        currTime = 0f;
    }
}

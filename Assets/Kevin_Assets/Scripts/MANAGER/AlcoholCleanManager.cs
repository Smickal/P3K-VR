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
    [SerializeField] Grabber _leftGrabber;
    [SerializeField] Grabber _rightGrabber;

    [HideInInspector] public bool IsHolding = false;

    bool isLeftCleaned = false;
    bool isRightCleaned = false;

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
        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {
        if (!IsHolding) return;

        currTime += Time.deltaTime;

        if (currentGrabber == _leftGrabber && currTime + currLeftTime > _timeToCleanGrabber)
        {
            isLeftCleaned = true;
            currTime = 0;
            IsHolding = false;

            //TRIGGER LEFT DONE HERE!
        }

        else if (currentGrabber == _rightGrabber && currTime + currRightTime > _timeToCleanGrabber)
        {
            isRightCleaned = true;
            currTime = 0;
            IsHolding = false;


            //TRIGGGER RIGHT DONE HERE!
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
}

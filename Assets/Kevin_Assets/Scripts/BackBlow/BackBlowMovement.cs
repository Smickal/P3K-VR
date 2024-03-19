using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using System;
using UnityEngine.Events;
using TMPro;

public class BackBlowMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _maxSmackVelocity;
    [SerializeField] float _minSmackVelocity;
    [SerializeField] float _backBlowCooldown = 0.25f;

    [Space(5)]
    [SerializeField] Grabbable _chestGrabable;
    [SerializeField] Grabber _leftGrabber;
    [SerializeField] Grabber _rightGrabber;
    [SerializeField] Transform _backColliderTrans;
    //

    [Header("debug")]
    [SerializeField] bool isDebug;
    [SerializeField] TMP_Text _debugText;
    Grabber currentGrabberForPull;
    Grabber currentGrabberForBackBlow;

    public static UnityEvent<Collider> OnBackBlow = new UnityEvent<Collider>();

    float curTime;
    float prevDistance;
    float curDistance;
    float velocity;

    float blowDistance;
    float prevBlowDistance;

    Vector3 startPos;
    Vector3 endPos;

    bool isGrabbingChest;
    bool isReducing;

    int backblowCount = 0;

    private void Start()
    {
        OnBackBlow.AddListener(CheckForBackBlowCollision);
    }

    private void Update()
    {
        if(isGrabbingChest)
        {
            prevDistance = curDistance;
            curDistance = Vector3.Distance(currentGrabberForBackBlow.transform.position, _backColliderTrans.position);

            prevBlowDistance = blowDistance;
            blowDistance = Vector3.Distance(currentGrabberForBackBlow.transform.position, currentGrabberForPull.transform.position);

            
            //check for distance Increasing
            if (prevDistance <= curDistance)
            {
                curTime = 0f;
                isReducing = false;

            }

            //Check for Distance Reducing
            else
            {
                curTime += Time.deltaTime;

                if(!isReducing)
                {
                    startPos = currentGrabberForBackBlow.transform.position;
                    isReducing = true;

                }
            }



        }
    }

    public void CheckForBackBlowCollision(Collider col)
    { 
        //CheckForChestPull
        if(currentGrabberForPull == null)
        {
            StopCalc();
            return;
        }

        endPos = currentGrabberForBackBlow.transform.position;

        float distance = Vector3.Distance(endPos, startPos);
        velocity = distance / curTime;


        if(currentGrabberForBackBlow && currentGrabberForPull && 
            velocity > _minSmackVelocity && velocity < _maxSmackVelocity
            && prevBlowDistance <= blowDistance)
        {
            backblowCount++;

            if(isDebug)
            {
                _debugText.SetText($"BackBlowCount = {backblowCount}.");
            }

            StopCoroutine(BackBlowCoolDown());
            StartCoroutine(BackBlowCoolDown());
        }
    }

    private void StartCalc()
    {
        isGrabbingChest = true;
    }

    private void StopCalc()
    {
        curTime = 0f;
        prevDistance = 0f;
        curDistance = 0f;
        prevBlowDistance = 0f;
        blowDistance = 0f;


        isGrabbingChest = false;
    }

    public void SetPullGrabber(Grabber grabber)
    {
        currentGrabberForPull = grabber;
        if (currentGrabberForPull == null) 
        {
            StopCalc();
            return;
        }

        if (grabber == _leftGrabber) currentGrabberForBackBlow = _rightGrabber;
        else if (grabber == _rightGrabber) currentGrabberForBackBlow = _leftGrabber;

        StartCalc();
    }
    
    IEnumerator BackBlowCoolDown()
    {
        StopCalc ();
        yield return new WaitForSeconds(_backBlowCooldown);
        StartCalc();
    }
}

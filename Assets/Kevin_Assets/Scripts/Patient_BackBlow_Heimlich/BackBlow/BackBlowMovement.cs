using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using System;
using UnityEngine.Events;
using TMPro;

public class BackBlowMovement : MonoBehaviour
{
    [SerializeField] private float _reducedScore, _fullScore, _totalScore;
    [SerializeField] PatientBackBlowHeimlich _patientBackBlowHeimlich;
    [Space(5)]
    // Start is called before the first frame update
    [SerializeField] float _maxSmackVelocity;
    [SerializeField] float _minSmackVelocity;
    [SerializeField] float _backBlowCooldown = 0.25f;

    [Space(5)]
    [Header("Controller GameObject")]
    [SerializeField] Collider _leftGrabber;
    [SerializeField] Collider _rightGrabber;
    [SerializeField] Collider _leftGrabberFull;
    [SerializeField] Collider _rightGrabberFull;

    [Header("HandTrack GameObject")]
    [SerializeField] Collider _leftGrabberHandTrack;
    [SerializeField] Collider _rightGrabberHandTrack;
    [SerializeField] Collider _leftGrabberHandTrackFull;
    [SerializeField] Collider _rightGrabberHandTrackFull;


    [SerializeField] Transform _backColliderTrans;
    [SerializeField] private BackBlowFullCollider _bbFull;
    private bool enterNormalCollider;
    public bool EnterNormalCollider { get { return enterNormalCollider; } }
    //

    [Header("debug")]
    [SerializeField] bool isDebug;
    [SerializeField] TMP_Text _debugText;
    GameObject currentGrabberForPull;
    GameObject currentGrabberForBackBlow;

    public static UnityEvent<Collider> OnBackBlow = new UnityEvent<Collider>();
    public static UnityEvent<Collider> OnReleaseBackBlow = new UnityEvent<Collider>();


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
    bool isHittingCollider;
    Collider currentHitCollider;


    int backblowCount = 0;
    private void Start()
    {
        OnBackBlow.AddListener(CheckForBackBlowCollision);
        OnReleaseBackBlow.AddListener(CheckForExitingCollider);
    }

    private void Update()
    {
        if(isGrabbingChest)
        {
            prevDistance = curDistance;
            if(currentGrabberForPull == null)
            {
                StopCalc();
                return;
            }

            curDistance = Vector3.Distance(currentGrabberForBackBlow.transform.position, _backColliderTrans.position);

            prevBlowDistance = blowDistance;
            if(currentGrabberForPull == null)
            {
                StopCalc();
                return;
            }
            
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
        
        if (isHittingCollider) return;
        // Debug.Log("Di siniaaaaaaaabbbbbbbbaaaaaaaaaaaaaaaaaabb ???");
        bool isFullScores = false;

        // Debug.Log(col.gameObject == _rightGrabberFull);
        // Debug.Log(" " + col);

        // if(!(col.gameObject == _leftGrabberFull || col.gameObject == _rightGrabberFull || col == _leftGrabber || col == _rightGrabber)) return;
        if(!InteractToolsController.CheckIsHandTrackOn())
        {
            if(col == _leftGrabberFull || col == _rightGrabberFull || col == _leftGrabber || col == _rightGrabber)
            {
                isHittingCollider = true;
                if(col == _leftGrabberFull)
                {
                    currentHitCollider = _leftGrabber;
                    isFullScores = true;
                }
                else if(col == _rightGrabberFull)
                {
                    currentHitCollider = _rightGrabber;
                    isFullScores = true;
                }
                else
                {
                    currentHitCollider = col;
                }
            }
            else return;
        }
        else
        {
            // bool yes = col == _rightGrabberHandTrack;
            // Debug.Log(col.gameObject.transform.parent.name + " Bener dongg" + _rightGrabberHandTrack.gameObject + this.gameObject);
            if(col == _leftGrabberHandTrackFull || col == _rightGrabberHandTrackFull || col == _leftGrabberHandTrack || col == _rightGrabberHandTrack)
            {
                isHittingCollider = true;
                if(col == _leftGrabberHandTrackFull)
                {
                    currentHitCollider = _leftGrabberHandTrack;
                    isFullScores = true;
                }
                else if(col == _rightGrabberHandTrackFull)
                {
                    currentHitCollider = _rightGrabberHandTrack;
                    isFullScores = true;
                }
                else
                {
                    currentHitCollider = col;
                }
            }
            else return;
        }
        
        Debug.Log("Di siniKAHHH ???"); 

        //CheckForChestPull
        if(currentGrabberForPull == null)
        {
            StopCalc();
            return;
        }
        Debug.Log("Di siniKAHHHAAAAAAAA ???"); 
        endPos = currentGrabberForBackBlow.transform.position;

        float distance = Vector3.Distance(endPos, startPos);
        velocity = distance / curTime;

        Debug.Log(velocity + " dan " + distance + " dan " + curTime); 
        if(currentGrabberForBackBlow && currentGrabberForPull && 
            velocity > _minSmackVelocity && velocity < _maxSmackVelocity
            && prevBlowDistance <= blowDistance)
        {
            
            // Debug.Log(col +"yang masuk"); 
            float scoreTemp = 0;
            backblowCount++;
            Debug.Log("ReducedBackBlow_Shoulder" + _reducedScore/2f);
            scoreTemp = (_reducedScore/2f);
            if(_bbFull.HitFull)
            {
                if(isFullScores)
                {
                    // backblowCount++;
                    Debug.Log("FullBackBlow_BB" + _fullScore);
                    scoreTemp = _fullScore;
                }

                //TODO: DROP REDUCED PROGGRESS HERE!
                else
                {
                    // backblowCount++;
                    Debug.Log("ReducedBackBlow_BB" + _reducedScore);
                    scoreTemp = _reducedScore;
                }
            }
            _totalScore += scoreTemp;
            enterNormalCollider = false;
                // Debug.Log("ReducedBackBlow_Back");
                //TODO: DROP FULL PROGGRESS HERE!
                

            if(isDebug)
            {
                _debugText.SetText($"Counter Backblow : {backblowCount}.");
                _patientBackBlowHeimlich?.OnBackBlowCountUp.Invoke( backblowCount, _totalScore);
            }

            StopCoroutine(BackBlowCoolDown());
            StartCoroutine(BackBlowCoolDown());
        }
    }

    public void CheckForExitingCollider(Collider collider)
    {

        //Check if the ones triggered is the same as the ones exiting
        if (currentHitCollider == collider)
        {
            isHittingCollider = false;
            currentHitCollider = null;
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

    public void SetPullGrabber(GameObject grabber)
    {
        
        currentGrabberForPull = grabber;
        if (currentGrabberForPull == null) 
        {
            StopCalc();
            return;
        }
        if(!InteractToolsController.CheckIsHandTrackOn())
        {
            if (grabber == _leftGrabber.gameObject) currentGrabberForBackBlow = _rightGrabber.gameObject;
            else if (grabber == _rightGrabber.gameObject) currentGrabberForBackBlow = _leftGrabber.gameObject;
        }
        else
        {
            if (grabber == _leftGrabberHandTrack.gameObject) currentGrabberForBackBlow = _rightGrabberHandTrack.gameObject;
            else if (grabber == _rightGrabberHandTrack.gameObject) currentGrabberForBackBlow = _leftGrabberHandTrack.gameObject;
        }
        StartCalc();
        
        
    }
    
    IEnumerator BackBlowCoolDown()
    {
        StopCalc ();
        yield return new WaitForSeconds(_backBlowCooldown);
        StartCalc();
    }

    public void ResetCount()
    {
        backblowCount = 0;
        _totalScore = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using System;
using UnityEngine.Events;
using TMPro;
using Meta.Voice.TelemetryUtilities;

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
    [SerializeField] Grabbable _chestGrabable;
    [SerializeField] Grabber _leftGrabber;
    [SerializeField] Grabber _rightGrabber;
    [SerializeField] GameObject _leftGrabberFull;
    [SerializeField] GameObject _rightGrabberFull;
    [SerializeField] Collider _leftGrabberColl;
    [SerializeField] Collider _rightGrabberColl;
    [SerializeField] Transform _backColliderTrans;
    [SerializeField] private BackBlowFullCollider _bbFull;
    private bool enterNormalCollider;
    public bool EnterNormalCollider { get { return enterNormalCollider; } }
    //

    [Header("debug")]
    [SerializeField] bool isDebug;
    [SerializeField] TMP_Text _debugText;
    Grabber currentGrabberForPull;
    Grabber currentGrabberForBackBlow;

    public static UnityEvent<Collider, bool> OnBackBlow = new UnityEvent<Collider, bool>();
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

    public void CheckForBackBlowCollision(Collider col, bool hitShoulderBlades)
    {
        // Debug.Log("Di siniaaaaaaaabbbbbbbbaaaaaaaaaaaaaaaaaabb ???");
        if (isHittingCollider) return;
        // Debug.Log("Di siniaaaaaaaa ???" + col);
        if(!(col.gameObject == _leftGrabber.gameObject || col.gameObject == _rightGrabber.gameObject || col.gameObject == _leftGrabberFull || col.gameObject == _rightGrabberFull)) return;

        enterNormalCollider = true;
        // if(!(col.gameObject == _leftGrabberFull || col.gameObject == _rightGrabberFull)) return;
        // Debug.Log("Di siniaaaaaaaabbbbbbbbbb ???");
        col.TryGetComponent<FullScoreBlow>(out FullScoreBlow fullScoreBlow);
        bool isFullScores = false;
        // Debug.Log("Di sini ???");


        if (fullScoreBlow != null) isFullScores = true;

        if(col.gameObject == _leftGrabberFull)
        {
            currentHitCollider = _leftGrabberColl;
        }
        else if(col.gameObject == _rightGrabberFull)
        {
            currentHitCollider = _rightGrabberColl;
        }
        else
        {
            currentHitCollider = col;
        }
        
        isHittingCollider = true;


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
            
            // Debug.Log("halo ???");
            // if(!hitShoulderBlades)
            // {
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
                
            // }
            // else
            // {
                
            // }
            

            if(isDebug)
            {
                _debugText.SetText($"BackBlowCount = {backblowCount}.");
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

    public void SetPullGrabber(Grabber grabber)
    {
        if(!InteractToolsController.CheckIsHandTrackOn())
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

using BNG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeimlichMovement : MonoBehaviour
{
    [SerializeField] private float _reducedScore, _fullScore, _totalScore;
    [SerializeField] PatientBackBlowHeimlich _patientBackblowHeim;

    [Header("Smack")]
    [SerializeField] float _minSmackVelocity;
    [SerializeField] float _maxSmackVelocity;

    [Header("Grabber - Controller")]
    [SerializeField] GameObject _leftGrabber;
    [SerializeField] GameObject _rightGrabber;

    [Header("Grabber - HT")]
    [SerializeField] GameObject _leftGrabberHT;
    [SerializeField] GameObject _rightGrabberHT;

    [Header("Grabbable")]
    [SerializeField] Grabbable _leftGrabbable;
    [SerializeField] Grabbable _rightGrabbable;
    
    [Header("TargetRelated")]
    [SerializeField] Transform _targetTr;
    [SerializeField] Transform _heimlichTargetColliderTr;
    [SerializeField] Transform _targetMaxMovTr;
    [SerializeField] Vector3 _targetTRStartPos;

    [Header("ConstraintMovement")]
    [SerializeField] LegTargetConstraint _targetConstraint;
    [Header("Ref For Visual")]
    [SerializeField]private GameObject leftVisual;
    [SerializeField]private GameObject rightVisual, leftVisualHT, rightVisualHT;
    [SerializeField]private GameObject modelLeft, modelRight;
    private bool hasTurnOff;

    [Header("Debug")]
    [SerializeField] bool _isDebug = true;
    [SerializeField] TMP_Text _debugText;

    GameObject curLeftGrabber;
    GameObject curRightGrabber;

    private bool isGrabbing;

    private bool isLeftHandHit;
    private bool isRightHandHit;
    bool isReducing;

    float curTime;
    float prevDistance;
    float curDistance;
    float velocity;

    float heimlichDistance;
    float prevHeimlichDistance;

    int heimlichCount = 0;

    Vector3 startPos;
    Vector3 endPos;
    private bool isAlreadyScoring;
    [SerializeField]private HeimlichColliderFull _heimlichColliderFull;

    public bool IsGrabbing {  get { return isGrabbing; } }

    // Update is called once per frame
    public void Start()
    {
        modelRight.SetActive(false);
        modelLeft.SetActive(false);
    }
    void Update()
    {
        CheckGrabber();
        if(IsGrabbing)
        {
            //calculate middle VEctor
            float midYAxis = (_leftGrabbable.transform.position.y + _rightGrabbable.transform.position.y) / 2f;
            float midZAxis = (_leftGrabbable.transform.position.z + _rightGrabbable.transform.position.z) / 2f;

            //move Target According to left&right grabber
            _targetTr.position = new Vector3(_targetTr.position.x, midYAxis, midZAxis);

            //Calculate TargetMovement velocity
            prevDistance = curDistance;
            curDistance = Vector3.Distance(_targetTr.position, _heimlichTargetColliderTr.position);

            prevHeimlichDistance = heimlichDistance;
            heimlichDistance = Vector3.Distance(_targetMaxMovTr.position, _targetTr.position);


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

                if (!isReducing)
                {
                    startPos = _targetTr.position;
                    isReducing = true;

                }
            }
        }


        
    }


    public void SetGrabber(GameObject grabber)
    {
        if(grabber == null) return;
        if(InteractToolsController.CheckIsHandTrackOn == null)return;
        if(!InteractToolsController.CheckIsHandTrackOn())
        {
            if (grabber == _leftGrabber) curLeftGrabber = grabber;
            else if (grabber == _rightGrabber) curRightGrabber = grabber;
        }
        else
        {
            if (grabber == _leftGrabberHT) curLeftGrabber = grabber;
            else if (grabber == _rightGrabberHT) curRightGrabber = grabber;
        }
        
    }

    public void ReleaseGrabber(GameObject grabber)
    {
        if(grabber == null) return;
        if(InteractToolsController.CheckIsHandTrackOn == null)return;
        if(!InteractToolsController.CheckIsHandTrackOn())
        {
            if (grabber == _rightGrabber) curRightGrabber = null;
            else if(grabber == _leftGrabber) curLeftGrabber = null;
        }
        else
        {
            if (grabber == _leftGrabberHT) curLeftGrabber = null;
            else if (grabber == _rightGrabberHT) curRightGrabber = null;
        }
        

        //harusnya ini balikin ke asal
        //move Target According to left&right grabber
        if(hasTurnOff)
        {
            hasTurnOff = false;

            modelLeft.SetActive(false);
            modelRight.SetActive(false);
            
            leftVisual.SetActive(true);
            rightVisual.SetActive(true);
            leftVisualHT.SetActive(true);
            rightVisualHT.SetActive(true);
        }
        _targetTr.localPosition = _targetTRStartPos;
    }

    private void CheckGrabber()
    {
        if (curLeftGrabber && curRightGrabber)
        {
            isGrabbing = true;
            if(!hasTurnOff)
            {
                hasTurnOff = true;
                modelLeft.SetActive(true);
                modelRight.SetActive(true);

                leftVisual.SetActive(false);
                rightVisual.SetActive(false);
                leftVisualHT.SetActive(false);
                rightVisualHT.SetActive(false);
            }
            if(PlayerRestriction.IsRestrictMovement != null)
            {
                if(!PlayerRestriction.IsRestrictMovement())
                {
                    if(PlayerRestriction.ApplyMovementRestriction != null)PlayerRestriction.ApplyMovementRestriction();
                }
            }
        }
        
        else 
        {
            isGrabbing = false;
            if(hasTurnOff)
            {
                hasTurnOff = false;

                modelLeft.SetActive(false);
                modelRight.SetActive(false);
                
                leftVisual.SetActive(true);
                rightVisual.SetActive(true);
                leftVisualHT.SetActive(true);
                rightVisualHT.SetActive(true);
            }
        }
        
    }

    public int i = 0;
    public void CheckForTargetTrigger(Collider col, bool isFullScore)
    {
        //Must check for 2 hands + Target Trigger
        if (col == null) return;
        else if (!IsHandsTriggered()) return;
        // Debug.Log(col.gameObject + " Collider yang masuk sini");
        if(!(col.gameObject == _leftGrabbable.gameObject || col.gameObject == _rightGrabbable.gameObject)) return;
        // Debug.Log(col.gameObject + " Collider yang masuk sini2");
        if(isAlreadyScoring)
        {
            // Debug.Log("Masih ada");
            i++;
            return;
        }
        

        endPos = _targetTr.position;

        float distance = Vector3.Distance(endPos, startPos);
        velocity = distance / curTime;


        if (velocity > _minSmackVelocity && velocity < _maxSmackVelocity
            && prevHeimlichDistance > heimlichDistance && prevDistance > curDistance)
        {

            //NOTE: DROP FULL SCORE alias MAX PROGGRESS BAR HERE
            // if (isFullScore)
            // {
            //     heimlichCount++;
            //     Debug.Log("FULL PROGRESS_H");
            //     _totalScore += _fullScore;
            // }

            // //NOTE: DROP REDUCE SCORE PROGRESS HERE!
            // else
            // {
            //     heimlichCount++;
            //     Debug.Log("REDUCED PROGRESS_H");
            //     _totalScore += _reducedScore;
            // }
            float score = 0;
            heimlichCount++;
            // Debug.Log("REDUCED PROGRESS_H");
            score = _reducedScore;
            if(_heimlichColliderFull.HitFull)
            {
                // Debug.Log("FULL PROGRESS_H");
                score = _fullScore;
            }
            _totalScore += score;
            

            if (_isDebug)
            {
                _debugText.SetText($"Counter Heimlich : {heimlichCount}");
                _patientBackblowHeim?.OnHeimlichCountUp.Invoke(heimlichCount, _totalScore);
            }

            //reset hand trigger
            isLeftHandHit = false;
            isRightHandHit = false;
            isAlreadyScoring = false;

            //Reset
            velocity = 0f;
            prevDistance = 0f;
            curDistance = 0f;
            prevHeimlichDistance = 0f;
            heimlichDistance = 0f;
        }


    }

    public void CheckForGrababbleTrig(Collider col)
    {
        Grabbable grabber = col.GetComponent<Grabbable>();
        
        if (grabber == null) return;
        
        // Debug.Log("Collider HT Kah ? " + grabber.gameObject);
        if(grabber == _rightGrabbable)
        {
            isRightHandHit = true;
        }
        else if(grabber == _leftGrabbable)
        {
            isLeftHandHit = true;
        }


    }

    public void CheckForGrabbableExit(Collider col)
    {
        Grabbable grabber = col.GetComponent<Grabbable>();
        if (grabber == null) return;

        if (grabber == _rightGrabbable)
        {
            isRightHandHit = false;
        }
        else if (grabber == _leftGrabbable)
        {
            isLeftHandHit = false;
        }
    }

    public bool IsHandsTriggered()
    {
        return isLeftHandHit && isRightHandHit;
    }

    public void ResetCount()
    {
        heimlichCount = 0;
        _totalScore = 0;
    }
}

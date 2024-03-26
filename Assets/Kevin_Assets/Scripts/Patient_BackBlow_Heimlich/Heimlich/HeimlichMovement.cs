using BNG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeimlichMovement : MonoBehaviour
{
    [SerializeField] PatientBackBlowHeimlich _patientBackblowHeim;

    [Header("Smack")]
    [SerializeField] float _minSmackVelocity;
    [SerializeField] float _maxSmackVelocity;

    [Header("Grabber")]
    [SerializeField] Grabber _leftGrabber;
    [SerializeField] Grabber _rightGrabber;

    [Header("Grabbable")]
    [SerializeField] Grabbable _leftGrabbable;
    [SerializeField] Grabbable _rightGrabbable;
    
    [Header("TargetRelated")]
    [SerializeField] Transform _targetTr;
    [SerializeField] Transform _heimlichTargetColliderTr;
    [SerializeField] Transform _targetMaxMovTr;

    [Header("Debug")]
    [SerializeField] bool _isDebug = true;
    [SerializeField] TMP_Text _debugText;

    Grabber curLeftGrabber;
    Grabber curRightGrabber;

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

    public bool IsGrabbing {  get { return isGrabbing; } }

    // Update is called once per frame
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


    public void SetGrabber(Grabber grabber)
    {
        if(grabber == null) return;

        if (grabber == _leftGrabber) curLeftGrabber = grabber;
        else if (grabber == _rightGrabber) curRightGrabber = grabber;
    }

    public void ReleaseGrabber(Grabber grabber)
    {
        if(grabber == null) return;

        if (grabber == _rightGrabber) curRightGrabber = null;
        else if(grabber == _leftGrabber) curLeftGrabber = null;
    }

    private void CheckGrabber()
    {
        if (curLeftGrabber && curRightGrabber) isGrabbing = true;
        else isGrabbing = false;
    }


    public void CheckForTargetTrigger(Collider col, bool isFullScore)
    {
        //Must check for 2 hands + Target Trigger
        if (col == null) return;
        else if (!IsHandsTriggered()) return;

        endPos = _targetTr.position;

        float distance = Vector3.Distance(endPos, startPos);
        velocity = distance / curTime;


        if (velocity > _minSmackVelocity && velocity < _maxSmackVelocity
            && prevHeimlichDistance > heimlichDistance && prevDistance > curDistance)
        {

            //NOTE: DROP FULL SCORE alias MAX PROGGRESS BAR HERE
            if (isFullScore)
            {
                heimlichCount++;
                Debug.Log("FULL PROGRESS");
            }

            //NOTE: DROP REDUCE SCORE PROGRESS HERE!
            else
            {
                heimlichCount++;
                Debug.Log("REDUCED PROGRESS");
            }
            

            if (_isDebug)
            {
                _debugText.SetText($"HEIMLICHCount = {heimlichCount}.");
                _patientBackblowHeim?.OnHeimlichCountUp.Invoke(heimlichCount);
            }

            //reset hand trigger
            isLeftHandHit = false;
            isRightHandHit = false;

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
}

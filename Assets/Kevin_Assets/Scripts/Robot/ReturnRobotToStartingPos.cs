using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ReturnRobotToStartingPos : MonoBehaviour
{
    [SerializeField]GameManager gameManager;
    [SerializeField] Transform _startingPos;
    public Transform StartingPos{get{return _startingPos;}}
    [SerializeField] float _lerpSpeed = 15f;
    [SerializeField] float _snapDistance = 0.05f;

    [SerializeField] Grabber _leftGrabber;
    [SerializeField] Grabber _rightGrabber;
    

    
    RobotAnimationController _controller;
    Rigidbody rigid;
    Grabbable grabbable;
    IsBeingGrabHandTrack isBeingGrabHandTrack;
    Robot robot;
    bool isMovingToSnapZone = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        grabbable = GetComponent<Grabbable>();
        isBeingGrabHandTrack = GetComponent<IsBeingGrabHandTrack>();
        robot = GetComponent<Robot>();
        _controller = GetComponent<RobotAnimationController>();
        
        transform.position = _startingPos.position;
    }

    private void Update()
    {
        if (robot.IsFollowingPlayer || gameManager.GameStateNow() == GameState.Cinematic) return;
        if(isMovingToSnapZone)
        {
            if(rigid.constraints == RigidbodyConstraints.FreezeAll)
            {
                rigid.constraints = RigidbodyConstraints.None;
            }
            Vector3 moveDir = _startingPos.position - transform.position;

            //rigid.AddForce(moveDir * Time.deltaTime * _lerpSpeed, ForceMode.Force);
            rigid.velocity = moveDir * Time.deltaTime * _lerpSpeed;

            //transform.position = Vector3.MoveTowards(transform.position, _startingPos.transform.position, Time.deltaTime * _lerpSpeed);



            if (Vector3.Distance(transform.position, _startingPos.transform.position) <= _snapDistance)
            {
                _controller.TriggerIdleAnim();

                isMovingToSnapZone = false;
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = Vector3.zero;
                transform.position = _startingPos.transform.position;
            }   
        }
        else
        {
            if (Vector3.Distance(transform.position, _startingPos.transform.position) <= _snapDistance) return;
            // Debug.Log("hayoo");
            if ((_leftGrabber.RemoteGrabbingGrabbable == grabbable || _rightGrabber.RemoteGrabbingGrabbable == grabbable)) return;
            // Debug.Log(grabbable.BeingHeld + "hayooooloooo" + isBeingGrabHandTrack.IsBeingGrab());
            if (grabbable.BeingHeld) return;
            // Debug.Log("yay");
            isMovingToSnapZone = true;
        }


        
    }

    public void MoveToSnapZone()
    {
        isMovingToSnapZone = true;
    }

    public void StopMoveToSnapZone()
    {
        isMovingToSnapZone = false;
    }

    public void SetStartingPos(Transform newPos)
    {
        _startingPos = newPos;
        transform.position = _startingPos.transform.position;
    }
    public void OnlySetStartPos(Transform newPos)
    {
        _startingPos = newPos;
    }
}

using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnRobotToStartingPos : MonoBehaviour
{
    [SerializeField] Transform _startingPos;
    [SerializeField] float _lerpSpeed = 15f;
    [SerializeField] float _snapDistance = 0.05f;

    [SerializeField] Grabber _leftGrabber;
    [SerializeField] Grabber _rightGrabber; 

    
    Rigidbody rigid;
    Grabbable grabbable;
    Robot robot;
    bool isMovingToSnapZone = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        grabbable = GetComponent<Grabbable>();
        robot = GetComponent<Robot>();
    }

    private void Update()
    {
        if (robot.IsFollowingPlayer) return;

        if(isMovingToSnapZone)
        {
            Vector3 moveDir = _startingPos.position - transform.position;

            //rigid.AddForce(moveDir * Time.deltaTime * _lerpSpeed, ForceMode.Force);
            rigid.velocity = moveDir * Time.deltaTime * _lerpSpeed;

            //transform.position = Vector3.MoveTowards(transform.position, _startingPos.transform.position, Time.deltaTime * _lerpSpeed);



            if (Vector3.Distance(transform.position, _startingPos.transform.position) <= _snapDistance)
            {
                isMovingToSnapZone = false;
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = Vector3.zero;
                transform.position = _startingPos.transform.position;
            }   
        }
        else
        {
            if (Vector3.Distance(transform.position, _startingPos.transform.position) <= _snapDistance) return;
            if ((_leftGrabber.RemoteGrabbingGrabbable == grabbable || _rightGrabber.RemoteGrabbingGrabbable == grabbable)) return;
            if (grabbable.BeingHeld) return;

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
}

using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class Robot : GrabbableEvents
{
    [SerializeField] float _minDistanceToPlayer = 2.0f;
    [SerializeField] float _maxDistanceToPlayer = 3.0f;
    [SerializeField] float _followVelocity = 500f;
    [SerializeField] float _yOffset = 1f;
    [SerializeField] float _xOffset = 1.25f;


    RobotAnimationController _controller;
    ReturnRobotToStartingPos returnPos;
    Camera mainCam;
    Rigidbody myRb;

    [Header("Debug")]
    [SerializeField] bool isActivated = false;
    [SerializeField] bool isFollowingPlayer = false;

    public bool IsFollowingPlayer {  get { return isFollowingPlayer; } }
    public void ActivateLookAt()
    {
        isActivated = true;
    }

    public void DeactivateLookAt()
    {
        isActivated = false;
    }




    private void Start()
    {
        _controller = GetComponent<RobotAnimationController>();
        myRb = GetComponent<Rigidbody>();
        returnPos = GetComponent<ReturnRobotToStartingPos>();
        mainCam = Camera.main;
        ActivateLookAt();
    }

    private void Update()
    {

        //Robot will lookat camera when positioned in his snapzone.
        if(isActivated)
        {
            transform.LookAt(mainCam.transform);
        }

        //Checks if the player Stops grabbing in the middle of animation.
        if (grab.RemoteGrabbing)
        {
            DeactivateLookAt();
            returnPos.StopMoveToSnapZone();
        }


        //Tries to stay in front of the camera
        if (!isFollowingPlayer || grab.RemoteGrabbing || grab.BeingHeld) return;
        ActivateLookAt();

        myRb.velocity = Vector3.zero;
        myRb.angularVelocity = Vector3.zero;


        Vector3 targetPos = mainCam.transform.position +
                (mainCam.transform.forward * _minDistanceToPlayer) +
                (mainCam.transform.right * _xOffset) +
                    (mainCam.transform.up * _yOffset);
        Debug.Log(targetPos + " target ");

        if(Vector3.Distance(mainCam.transform.position, transform.position) > _minDistanceToPlayer &&
            Vector3.Distance(mainCam.transform.position, transform.position) < _maxDistanceToPlayer)
        {
            Vector3 robotCameraDistance = transform.position - mainCam.transform.position;
            float dot = Vector3.Dot(robotCameraDistance, mainCam.transform.forward);
            if(dot > 0)
            {
                Debug.Log("Ga di dalem kamera woi");
            }
            else
            {
                Debug.Log("Di dalem kamera woi");
                _controller.TriggerIdleAnim();
            }
            
        }

        else if (Vector3.Distance(mainCam.transform.position, transform.position) > _maxDistanceToPlayer)
        {
            transform.position += (targetPos - transform.position) * 0.025f;
            _controller.TriggerForwardAnim();
        }

        else if (Vector3.Distance(mainCam.transform.position, transform.position) < _minDistanceToPlayer)
        {
            transform.position += (targetPos - transform.position) * 0.025f;
            _controller.TriggerBackwardAnim();

        }


    }


    public override void OnGrab(Grabber grabber)
    {
        _controller.TriggerFrozeAnim();

        thisGrabber = grabber;
        DeactivateLookAt();
        returnPos.StopMoveToSnapZone();
    }


    public override void OnRelease()
    {
        _controller.TriggerBackwardAnim();

        ActivateLookAt();
        returnPos.MoveToSnapZone();
    }


    public void ReleaseRobot()
    {
        if(GameManager.CheckGameStateNow() == GameState.Cinematic)
        {
            return;
        }
        ActivateLookAt();
        grab.DropItem(thisGrabber, true, false);
        returnPos.MoveToSnapZone();
    }

    public void ActivateFollowPlayer()
    {
        isFollowingPlayer = true;
    }

    public void DeactivateFollowPlayer()
    {
        isFollowingPlayer = false;
    }

}

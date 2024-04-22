using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class Robot : GrabbableEvents
{
    [SerializeField] float _distanceToPlayer = 3.0f;
    [SerializeField] float _followVelocity = 500f;
    [SerializeField] float _fov = 60f;
    [SerializeField] Transform _playerTransform;

    ReturnRobotToStartingPos returnPos;
    Camera mainCam;
    Rigidbody myRb;

    [Header("Debug")]
    [SerializeField] bool isActivated = false;
    [SerializeField] bool isFollowingPlayer = false;
    [SerializeField] bool isCentered = false;

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
        else if (grab.RemoteGrabbing)
        {
           DeactivateLookAt();
           returnPos.StopMoveToSnapZone();
        }


        Vector3 rayDir = transform.position - mainCam.transform.position;
        RaycastHit hit;

        //if(Physics.Raycast())
        //Tries to stay in front of the camera
        if (isFollowingPlayer && !isCentered)
        {
            Vector3 targetPos = mainCam.transform.position + (mainCam.transform.forward * _distanceToPlayer);
            if (Vector3.Angle(rayDir, mainCam.transform.forward) > _fov)
            {
                transform.position += (targetPos - transform.position) * 0.025f;
            }

            if (Vector3.Distance(mainCam.transform.position, transform.position) > _distanceToPlayer)
            {
                transform.position += (targetPos - transform.position) * 0.025f;
            }

            else if(Vector3.Distance(mainCam.transform.position, transform.position) < _distanceToPlayer)
            {
                transform.position += (targetPos - transform.position) * 0.025f;
            }

        }



    }

    


    public override void OnGrab(Grabber grabber)
    {

        thisGrabber = grabber;
        DeactivateLookAt();
        returnPos.StopMoveToSnapZone();
    }


    public override void OnRelease()
    {
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
}

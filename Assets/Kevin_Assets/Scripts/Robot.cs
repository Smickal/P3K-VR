using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Robot : GrabbableEvents
{
    ReturnRobotToStartingPos returnPos;
    Camera mainCam;

    bool isActivated = false;
    public void ActivateLookAt()
    {
        isActivated = true;
    }
    private void Start()
    {
        returnPos = GetComponent<ReturnRobotToStartingPos>();
        mainCam = Camera.main;
        ActivateLookAt();
    }

    private void Update()
    {
        if(isActivated)
        {
            transform.LookAt(mainCam.transform);
        }

    }

    public void DeactivateLookAt() 
    { 
        isActivated = false; 
    }


    public override void OnGrab(Grabber grabber)
    {
        thisGrabber = grabber;

        DeactivateLookAt();
        
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

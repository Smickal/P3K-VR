using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : GrabbableEvents
{
    Camera mainCam;



    bool isActivated = false;
    public void ActivateLookAt()
    {
        isActivated = true;
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
    }

    private void Start()
    {
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
}

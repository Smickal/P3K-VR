using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class HeimlichGrabbableEvent : GrabbableEvents
{
    [SerializeField] HeimlichMovement _heimlichMovement;


    Grabber grabber;
    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);

        _heimlichMovement.SetGrabber(grabber);
        this.grabber = grabber;
    }

    public override void OnRelease()
    {
        base.OnRelease();

        _heimlichMovement.ReleaseGrabber(grabber);
    }
}

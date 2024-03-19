using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class ChestPullGrabbableEvent : GrabbableEvents
{
    [SerializeField] BackBlowMovement _bacBlowMov;

    public override void OnGrab(Grabber grabber)
    {
        thisGrabber = grabber;

        _bacBlowMov.SetPullGrabber(grabber);
    }

    public override void OnRelease()
    {
        base.OnRelease();

        _bacBlowMov.SetPullGrabber(null);
    }
}

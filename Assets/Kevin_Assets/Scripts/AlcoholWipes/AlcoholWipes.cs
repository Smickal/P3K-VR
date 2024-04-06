using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholWipes : GrabbableEvents
{
    Grabber curGrabber;

    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);

        curGrabber = grabber;
        AlcoholCleanManager.Instance.RegisterGrabber(grabber);
    }

    public override void OnGrip(float gripValue)
    {
        base.OnGrip(gripValue);

        if (!curGrabber) return;
        AlcoholCleanManager.Instance.IsHolding = true;

    }

    public override void OnRelease()
    {
        base.OnRelease();
        curGrabber = null;

        AlcoholCleanManager.Instance.IsHolding = false;
        AlcoholCleanManager.Instance.SaveCurrentTimeProgress();
    }



}

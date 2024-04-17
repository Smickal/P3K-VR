using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholWipes : GrabbableEvents
{
    Grabber curGrabber;

    AlcoholCleanManager alcoholManager;

    private void Start()
    {
        alcoholManager = AlcoholCleanManager.Instance;
    }

    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);

        curGrabber = grabber;
        alcoholManager?.RegisterGrabber(grabber);
    }

    public override void OnGrip(float gripValue)
    {
        base.OnGrip(gripValue);

        if (!curGrabber) return;
        alcoholManager.IsHolding = true;

    }

    public override void OnRelease()
    {
        base.OnRelease();
        curGrabber = null;

        alcoholManager.IsHolding = false;
        alcoholManager.SaveCurrentTimeProgress();
    }



}

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
        if(AlcoholCleanManager.Instance)AlcoholCleanManager.Instance.RegisterGrabber(grabber);
    }

    public override void OnGrip(float gripValue)
    {
        base.OnGrip(gripValue);

        if (!curGrabber) return;
        if(AlcoholCleanManager.Instance)AlcoholCleanManager.Instance.IsHolding = true;

    }

    public override void OnRelease()
    {
        base.OnRelease();
        curGrabber = null;

        if(AlcoholCleanManager.Instance)AlcoholCleanManager.Instance.IsHolding = false;
        if(AlcoholCleanManager.Instance)AlcoholCleanManager.Instance.SaveCurrentTimeProgress();
    }



}

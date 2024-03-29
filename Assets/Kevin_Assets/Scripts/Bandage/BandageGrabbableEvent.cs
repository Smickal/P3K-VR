using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class BandageGrabbableEvent : GrabbableEvents
{
    [SerializeField] Bandage _bandage;


    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);


        if(_bandage.BandageMovement != null)
            _bandage.BandageMovement.ActivateBandageMovement();
    }

    public override void OnRelease()
    {
        base.OnRelease();


        if(_bandage.BandageMovement != null)
            _bandage.BandageMovement.DisableBandageMovement();
    }
}

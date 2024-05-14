using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class ChestPullGrabbableEvent : GrabbableEvents
{
    [SerializeField] BackBlowMovement _bacBlowMov;

    public override void OnGrab(Grabber grabber)
    {
        if(PlayerRestriction.IsRestrictMovement != null)
        {
            if(!PlayerRestriction.IsRestrictMovement())
            {
                if(PlayerRestriction.ApplyMovementRestriction != null)PlayerRestriction.ApplyMovementRestriction();
            }
        }
        
        
        _bacBlowMov.SetPullGrabber(grabber.gameObject);
    }

    public override void OnRelease()
    {
        base.OnRelease();
        if(PlayerRestriction.IsRestrictMovement != null)
        {
            if(PlayerRestriction.IsRestrictMovement())
            {
                if(PlayerRestriction.LiftMovementRestriction != null)
                {
                    PlayerRestriction.LiftMovementRestriction();
                }
            }
        }
        
        _bacBlowMov.SetPullGrabber(null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class GloveGrabbableEvent : GrabbableEvents
{
    [SerializeField] GloveChangeManager _manager;

    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);

        _manager.ChangeToGlove();
        grab.Release(Vector3.zero, Vector3.zero);
    }
}

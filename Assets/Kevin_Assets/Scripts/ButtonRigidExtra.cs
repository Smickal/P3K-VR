using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class ButtonRigidExtra : GrabbableEvents
{

    [SerializeField] GameObject _innerButtonContainer;
    [SerializeField] Rigidbody _parentRigid;

    FixedJoint fixedJoint;


    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);

        fixedJoint = _innerButtonContainer.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = _parentRigid;
    }


    public override void OnRelease()
    {
        base.OnRelease();
        Destroy(fixedJoint);
        fixedJoint = null;
    }
}

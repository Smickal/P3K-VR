using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class AlcoholWipesGrabbableEvent : GrabbableEvents
{
    [SerializeField]GameObject Lid, WipeOut, SnapPoint;
    [SerializeField]Collider Lid_Collider;

    private void Start() 
    {
        SnapPoint.SetActive(false);
    }
    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);
        Lid_Collider.enabled = false;
        WipeOut.SetActive(true);
        SnapPoint.SetActive(true);
        
        Lid.SetActive(false);
        // grabber.TryRelease();
        
        
    }
}

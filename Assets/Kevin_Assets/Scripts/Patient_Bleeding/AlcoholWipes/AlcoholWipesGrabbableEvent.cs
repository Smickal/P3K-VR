using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class AlcoholWipesGrabbableEvent : GrabbableEvents
{
    [SerializeField]GameObject Lid, WipeOut, SnapPoint;
    [SerializeField]Collider Lid_Collider, snapPoint_Collider, alcoholWipeCollider;

    private void Start() 
    {
        // SnapPoint.SetActive(false);
        snapPoint_Collider.enabled = false;
        alcoholWipeCollider.enabled = false;
    }
    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);
        Lid_Collider.enabled = false;
        WipeOut.SetActive(true);
        // SnapPoint.SetActive(true);
        snapPoint_Collider.enabled = true;
        alcoholWipeCollider.enabled = true;
        
        Lid.SetActive(false);
        StartCoroutine(GrabRelease(grabber));
        
        
    }
    IEnumerator GrabRelease(Grabber grabber)
    {
        yield return new WaitForSeconds(.1f);
        grabber.TryRelease();
    }
}

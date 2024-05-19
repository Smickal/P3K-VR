using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.Events;

public class AlcoholWipesGrabbableEvent : GrabbableEvents
{
    [SerializeField]GameObject Lid, WipeOut, SnapPoint;
    [SerializeField]Collider Lid_Collider;
    [SerializeField]SnapZone snapZonePoint;
    public UnityAction<Grabbable> awakeSnapWipe;
    private void Awake() 
    {
        awakeSnapWipe = TurnSnapPointOff;
        snapZonePoint.OnSnapEvent.AddListener(awakeSnapWipe);
    }

    public void TurnSnapPointOff(Grabbable grabbable)
    {
        if(snapZonePoint.HeldItem == null)return;
        SnapPoint.SetActive(false);
        snapZonePoint.OnSnapEvent.RemoveListener(awakeSnapWipe);
    }
    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);
        Lid_Collider.enabled = false;
        WipeOut.SetActive(true);
        SnapPoint.SetActive(true);
        // snapPoint_Collider.enabled = true;
        // alcoholWipeCollider.enabled = true;
        
        Lid.SetActive(false);
        StartCoroutine(GrabRelease(grabber));
        
        
    }
    IEnumerator GrabRelease(Grabber grabber)
    {
        yield return new WaitForSeconds(.1f);
        grabber.TryRelease();
    }
}

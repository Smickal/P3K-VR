using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitP3K : GrabbableEvents
{
    [SerializeField] SOKotakP3K _scriptableData;
    [SerializeField]IsBeingGrabHandTrack isBeingGrabHandTrack;
    private bool wasGrabHT = false;
    private void Start() 
    {
        isBeingGrabHandTrack = GetComponent<IsBeingGrabHandTrack>();
    }
    public override void OnTriggerDown()
    {
        // Debug.Log(_scriptableData.KitName+ "huh");
        UIKotakP3K.CheckUnlock(_scriptableData);

        base.OnTriggerDown();
    }

    public override void OnGrab(Grabber grabber)
    {
            // Debug.Log("is this first?");
            UIKotakP3K.CheckUnlock(_scriptableData);
            if(GameManager.CheckLevelModeNow() == LevelMode.Home)UIKotakP3K.OpenDescriptioninRoom(_scriptableData);
    }

    public override void OnRelease()
    {
        if(GameManager.CheckLevelModeNow() == LevelMode.Home)UIKotakP3K.CloseDescriptioninRoom(_scriptableData);
    }
    public void OnGrabHT()
    {
        if(isBeingGrabHandTrack.IsBeingGrab())
        {
            UIKotakP3K.CheckUnlock(_scriptableData);
            wasGrabHT = true;
            if(GameManager.CheckLevelModeNow != null && GameManager.CheckLevelModeNow() == LevelMode.Home)UIKotakP3K.OpenDescriptioninRoom(_scriptableData);
        }
    }
    public void OnReleaseHT()
    {
        if(wasGrabHT)
        {
            wasGrabHT = false;
            if(GameManager.CheckLevelModeNow != null && GameManager.CheckLevelModeNow() == LevelMode.Home)UIKotakP3K.CloseDescriptioninRoom(_scriptableData);
        }
        
    }

}

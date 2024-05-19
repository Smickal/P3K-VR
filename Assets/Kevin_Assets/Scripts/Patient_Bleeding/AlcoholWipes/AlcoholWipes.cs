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
        if(GameManager.CheckLevelTypeNow == null || GameManager.CheckInGameModeNow == null)return;
        if((GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || 
            GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid))return;
        base.OnGrab(grabber);

        curGrabber = grabber;
        alcoholManager?.RegisterGrabber(grabber.gameObject);
    }

    public override void OnGrip(float gripValue)
    {
        if(GameManager.CheckLevelTypeNow == null || GameManager.CheckInGameModeNow == null)return;
        if((GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || 
            GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid))return;
        base.OnGrip(gripValue);
    
        if (!curGrabber) return;
        alcoholManager.IsHolding = true;

    }

    public override void OnRelease()
    {
        base.OnRelease();
        curGrabber = null;
        if(alcoholManager == null)return;
        alcoholManager.IsHolding = false;
        alcoholManager.SaveCurrentTimeProgress();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class GloveGrabbableEvent : GrabbableEvents
{
    [SerializeField] GloveChangeManager _manager;
    
    [Header("Pas di luar rumah, Freeze All Rb")]
    public bool DebugOnly;

    public override void OnGrab(Grabber grabber)
    {
        base.OnGrab(grabber);

        if(!DebugOnly)
        {
            if(GameManager.CheckLevelTypeNow == null || GameManager.CheckInGameModeNow == null || BleedingWithoutEmbeddedItem.StateFirstAidNow != null)return;
            if((GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || 
            GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid || 
            BleedingWithoutEmbeddedItem.StateFirstAidNow() != BleedingWithoutEmbeddedItem_State.WearGloves))return;
        }
        

        if(_manager)_manager.ChangeToGlove();
        StartCoroutine(GrabRelease(grabber));
    }
    IEnumerator GrabRelease(Grabber grabber)
    {
        yield return new WaitForSeconds(.1f);
        grabber.TryRelease();
    }
}

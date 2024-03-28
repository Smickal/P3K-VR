using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitP3K : GrabbableEvents
{
    [SerializeField] SOKotakP3K _scriptableData;
    public override void OnTriggerDown()
    {
        // Debug.Log(_scriptableData.KitName+ "huh");
        UIKotakP3K.CheckUnlock(_scriptableData);

        base.OnTriggerDown();
    }

    public override void OnGrip(float gripValue)
    {
        if (gripValue > 0.8f)
        {
            // Debug.Log("is this first?");
            UIKotakP3K.CheckUnlock(_scriptableData);
            if(GameManager.CheckLevelModeNow() == LevelMode.Home)UIKotakP3K.OpenDescriptioninRoom(_scriptableData);
        }
    }

    public override void OnRelease()
    {
        if(GameManager.CheckLevelModeNow() == LevelMode.Home)UIKotakP3K.CloseDescriptioninRoom(_scriptableData);
    }

}

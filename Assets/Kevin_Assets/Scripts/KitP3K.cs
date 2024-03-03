using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitP3K : GrabbableEvents
{
    [SerializeField] SOKotakP3K _scriptableData;
    public override void OnTriggerDown()
    {
        UIKotakP3K.CheckUnlock(_scriptableData);

        base.OnTriggerDown();
    }

    public override void OnGrip(float gripValue)
    {
        if(gripValue > 0.8f)
        {
            UIKotakP3K.CheckUnlock(_scriptableData);
        }
    }
}

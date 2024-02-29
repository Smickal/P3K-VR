using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitP3K : GrabbableEvents
{
    [SerializeField] SOKotakP3K _scriptableData;
    public override void OnTriggerDown()
    {
        Debug.Log(_scriptableData.KitName);
        UIKotakP3K.CheckUnlock(_scriptableData);

        base.OnTriggerDown();
    }

}

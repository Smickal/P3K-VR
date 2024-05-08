using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class SnapZoneHandInteractablePositionChangeHelper : MonoBehaviour
{
    [SerializeField]SnapZone snapZone;
    [SerializeField]SnapZoneHandInteractablePositionChange helper;
    [SerializeField]List<string> OnlyAllowNames;
    
    public void ChangeWhenSnap()
    {
        if(snapZone.HeldItem == null)return;
        helper = snapZone.HeldItem.GetComponent<SnapZoneHandInteractablePositionChange>();

        if(helper != null)
        {
            if(!Allowed())return;
            helper.ChangeWhenScale();
        }
        
    }
    public void ChangeWhenUnSnap()
    {
        if(helper != null)
        {
            helper.ChangeBack();
            helper = null;
        }
    }
    public bool Allowed()
    {
        if (OnlyAllowNames != null && OnlyAllowNames.Count > 0) {
            string transformName = helper.transform.name;
            foreach(string allowName in OnlyAllowNames)
            {
                if (transformName.Contains(allowName)) {
                    return true;
                }
            }
        }
        return false;
    }
}

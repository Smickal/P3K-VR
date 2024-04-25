using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class IsBeingGrabHandTrack : MonoBehaviour
{
    [SerializeField]private bool isBeingGrabHandTrack;
    public void ChangeIsBeingGrab(bool change)
    {
        if(change)
        {
            if(transform.parent != null)
            {
                if(transform.parent.GetComponent<SnapZone>() != null)change = false;
            }
            
        }
        isBeingGrabHandTrack = change;
    }
    public bool IsBeingGrab()
    {
        return isBeingGrabHandTrack;
    }
}

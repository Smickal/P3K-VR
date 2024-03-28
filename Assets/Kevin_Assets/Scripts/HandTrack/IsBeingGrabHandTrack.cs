using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBeingGrabHandTrack : MonoBehaviour
{
    [SerializeField]private bool isBeingGrabHandTrack;
    public void ChangeIsBeingGrab(bool change)
    {
        isBeingGrabHandTrack = change;
    }
    public bool IsBeingGrab()
    {
        return isBeingGrabHandTrack;
    }
}

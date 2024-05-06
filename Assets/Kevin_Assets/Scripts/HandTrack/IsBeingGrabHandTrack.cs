using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BNG;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class IsBeingGrabHandTrack : MonoBehaviour
{
    [SerializeField]private bool isBeingGrabHandTrack;
    [SerializeField]private DistanceHandGrabInteractable[] distanceHandGrabInteractables;
    // private void Awake() 
    // {
    //     distanceHandGrabInteractables = transform.GetComponentsInChildren<DistanceHandGrabInteractable>(true).ToArray();
    // }
    public void ChangeIsBeingGrab(bool change)
    {
        bool changeDistance = true;
        if(change)
        {
            changeDistance = !change;
            if(transform.parent != null)
            {
                if(transform.parent.GetComponent<SnapZone>() != null)
                {
                    change = false;
                }
                
            }
            
        }
        isBeingGrabHandTrack = change;
        if(distanceHandGrabInteractables != null)
        {
            foreach(DistanceHandGrabInteractable distanceHandGrabInteractable in distanceHandGrabInteractables)
            {
                distanceHandGrabInteractable.enabled = changeDistance;
            }
        }
    }
    public bool IsBeingGrab()
    {
        return isBeingGrabHandTrack;
    }
}

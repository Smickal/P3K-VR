using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class BandageInteractableEvent : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]private HandGrabInteractable[] handGrabs;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    private HandGrabInteractor currHand;
    [SerializeField] Bandage _bandage;
    public void OnGrabHT()
    {
        HandGrabInteractor curHand = CheckHandGrabInteractor();
        if(curHand == null)return;
        currHand = curHand;
        if(_bandage.BandageMovement != null)
            _bandage.BandageMovement.ActivateBandageMovement();
    }

    public void OnReleaseHT()
    {
        if(currHand == null)return;
        currHand = null;
        if (_bandage.BandageMovement != null)
        {
            _bandage.BandageMovement.DisableBandageMovement();
            _bandage.BandageMovement.DeleteCustomPosMesh();
        }

    }
    private HandGrabInteractor CheckHandGrabInteractor()
    {
        foreach(HandGrabInteractable handGrabInteractable in handGrabs)
        {
            // Debug.Log("handgrab" + handGrabInteractable);
            if(handGrabInteractable.HasSelectingInteractor(leftGrabberHT))
            {
                // Debug.Log(handGrabInteractable + " This is the chosen onee");
                return leftGrabberHT;
            }
            else if (handGrabInteractable.HasSelectingInteractor(rightGrabberHT))
            {
                // Debug.Log(handGrabInteractable + " This is the chosen onee");
                return rightGrabberHT;
            }
        }
        return null;
    }
    public void ReleaseHandGrabNow()
    {
        if(currHand == null)return;
        currHand.ForceRelease();
    }
}

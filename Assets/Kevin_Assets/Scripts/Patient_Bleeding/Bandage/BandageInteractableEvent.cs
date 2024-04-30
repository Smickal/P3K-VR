using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class BandageInteractableEvent : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]private HandGrabInteractable[] handGrabs;
    [SerializeField]private DistanceHandGrabInteractable[] distanceHandGrabs;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    [SerializeField]private DistanceHandGrabInteractor leftDistanceGrabberHT, rightDistanceGrabberHT;
    private HandGrabInteractor currHand;
    private DistanceHandGrabInteractor currDistanceHand;
    [SerializeField] Bandage _bandage;
    private void Awake() {
        if(handGrabs == null)
        {
            handGrabs = GetComponentsInChildren<HandGrabInteractable>().ToArray();
        }
        if(distanceHandGrabs == null)
        {
            distanceHandGrabs = GetComponentsInChildren<DistanceHandGrabInteractable>().ToArray();
        }
    }
    public void OnGrabHT()
    {
        HandGrabInteractor curHand = CheckHandGrabInteractor();
        DistanceHandGrabInteractor curDistanceHand = CheckDistanceHandGrabInteractor();
        if(curHand == null && curDistanceHand == null)return;
        if(curHand != null)
        {
            currHand = curHand;
        }
        else if(curDistanceHand != null)
        {
            currDistanceHand = curDistanceHand;
        }
        
        if(_bandage.BandageMovement != null)
            _bandage.BandageMovement.ActivateBandageMovement();
    }

    public void OnReleaseHT()
    {
        if(currHand == null && currDistanceHand == null)return;
        currHand = null;
        currDistanceHand = null;
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
    private DistanceHandGrabInteractor CheckDistanceHandGrabInteractor()
    {
        foreach(DistanceHandGrabInteractable distanceHandGrabInteractable in distanceHandGrabs)
        {
            // Debug.Log("handgrab" + handGrabInteractable);
            if(distanceHandGrabInteractable.HasSelectingInteractor(leftDistanceGrabberHT))
            {
                // Debug.Log(distanceHandGrabInteractable + " This is the chosen onee distancee" + leftGrabberHT);
                return leftDistanceGrabberHT;
            }
            else if (distanceHandGrabInteractable.HasSelectingInteractor(rightDistanceGrabberHT))
            {
                // Debug.Log(distanceHandGrabInteractable + " This is the chosen onee distancee" + rightGrabberHT);
                return rightDistanceGrabberHT;
            }
        }
        return null;
    }
    public void ReleaseHandGrabNow()
    {
        if(currHand == null && currDistanceHand == null)return;
        if(currHand != null)currHand.ForceRelease();
        if(currDistanceHand != null)
        {
            currDistanceHand.Unselect();
            GetComponent<IsBeingGrabHandTrack>().ChangeIsBeingGrab(false);
            // distanceTemp.enabled = false;
        }
        
        currHand = null;
        currDistanceHand = null;
    }
}

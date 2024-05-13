using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BNG;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class BriefCaseInteractableEvent : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]BNG.Grabbable grabBNG;
    [SerializeField]private HandGrabInteractable[] handGrabs;
    [SerializeField]private DistanceHandGrabInteractable[] distanceHandGrabs;
    [SerializeField]private Collider[] briefColls;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    [SerializeField]private DistanceHandGrabInteractor leftDistanceGrabberHT, rightDistanceGrabberHT;
    private HandGrabInteractor currHand;
    private DistanceHandGrabInteractor currDistanceHand;
    [SerializeField]PlayerRestriction playerRestriction;
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
        
    }

    public void OnReleaseHT()
    {
        if(currHand == null && currDistanceHand == null)return;
        currHand = null;
        currDistanceHand = null;


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
        // Debug.Log("Yang ada isi adalah " + currHand + " dan " + currDistanceHand);
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
    public void TurnOffAll()
    {
        foreach(Collider collider in briefColls)
        {
            collider.enabled = false;
        }
        foreach(HandGrabInteractable grab in handGrabs)
        {
            playerRestriction.DeleteHandGrabInteractable(grab);
            grab.enabled = false;
        }
        foreach(DistanceHandGrabInteractable grab in distanceHandGrabs)
        {
            playerRestriction.DeleteDistanceHandGrab(grab);
            grab.enabled = false;
        }

        grabBNG.enabled = false;
        playerRestriction.DeleteGrabbable(grabBNG);
    }
}

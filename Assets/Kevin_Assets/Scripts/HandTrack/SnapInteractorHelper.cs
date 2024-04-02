using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;

public class SnapInteractorHelper : MonoBehaviour, ITurnOffStatic
{
    [SerializeField]private Oculus.Interaction.SnapInteractor snapInteractor;
    [SerializeField]private BNG.SnapZone snapZone;
    [SerializeField]private DistanceHandGrabInteractable distanceHandGrabInteractable;
    private void Awake() 
    {
        if(snapInteractor)snapInteractor.OnSelect += snapInteractor_OnSelect;
        if(snapInteractor)snapInteractor.OnUnSelect += snapInteractor_OnUnSelect;
        distanceHandGrabInteractable = transform.parent.GetComponentInChildren<DistanceHandGrabInteractable>();
    }

    public void TurnOffStatic()
    {
        if(snapInteractor)snapInteractor.OnSelect -= snapInteractor_OnSelect;
        if(snapInteractor)snapInteractor.OnUnSelect -= snapInteractor_OnUnSelect;
    }
    private void snapInteractor_OnUnSelect(object sender, Oculus.Interaction.SnapInteractor.OnUnSelectEventArgs e)
    {
        if(InteractToolsController.CheckIsHandTrackOn == null) return;
        if(InteractToolsController.CheckIsHandTrackOn())LeaveSnapZone(e.Interactable);

        if(distanceHandGrabInteractable)
        {
            distanceHandGrabInteractable.InSnapZone(false);
            distanceHandGrabInteractable.enabled = true;
        }
        
    }

    private void snapInteractor_OnSelect(object sender, Oculus.Interaction.SnapInteractor.OnSelectEventArgs e)
    {
        if(InteractToolsController.CheckIsHandTrackOn == null) return;
        if(InteractToolsController.CheckIsHandTrackOn())SetSnapZone(e.Interactable, e.Interactor);
        if(distanceHandGrabInteractable)
        {
            distanceHandGrabInteractable.enabled = false;
            distanceHandGrabInteractable.InSnapZone(true);
        }
    }

    public void ReviveSnapInteractorAfterReleaseController()
    {
        if(!snapInteractor.enabled)snapInteractor.enabled = true;
        snapInteractor.UnSetCandidate();
    }
    public void SetSnapZone(Oculus.Interaction.SnapInteractable interactable, Oculus.Interaction.SnapInteractor interactor)
    {
        // Debug.Log("Here" + OVRInput.IsControllerConnected(OVRInput.Controller.Hands) + interactor.name + " B " + interactable.name);
        BNG.Grabbable grab = interactor.GetComponentInParent<BNG.Grabbable>();
        snapZone = interactable.gameObject.GetComponent<BNG.SnapZone>();
        if(grab && snapZone)snapZone.GrabGrabbable_ForSnapHandTrackOnly(grab);
        // Debug.Log("what??");
    }
    public void LeaveSnapZone(Oculus.Interaction.SnapInteractable interactable)
    {
        // Debug.Log("Here" + OVRInput.IsControllerConnected(OVRInput.Controller.Hands));
        interactable.gameObject.GetComponent<BNG.SnapZone>().ReleaseAll_ForSnapHandTrackOnly();
        // Debug.Log("whatthe??");
    }
}

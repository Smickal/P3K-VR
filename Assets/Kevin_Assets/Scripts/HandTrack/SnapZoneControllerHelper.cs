using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class SnapZoneControllerHelper : MonoBehaviour
{
    [SerializeField]private BNG.SnapZone grabbable;
    [SerializeField]private Oculus.Interaction.SnapInteractable chosenInteractable;
    private Oculus.Interaction.SnapInteractor snapInteractor;
    
    public void SnapSelected_ConnectToHandTrackSnap()
    {
        
        snapInteractor = grabbable.HeldItem.GetComponentInChildren<Oculus.Interaction.SnapInteractor>();
        if(!snapInteractor.enabled)snapInteractor.enabled = true;
        snapInteractor.SetCandidate(chosenInteractable);
        Debug.Log(this.gameObject + " muncul pas start - connect");
        
    }
    public void SnapUnSelect_DisconnectHandTrackSnap()
    {
        if(snapInteractor != null)
        {
            snapInteractor.UnSetCandidate();
            snapInteractor.enabled = false;
        
            snapInteractor = null;
        }
        
    }

}

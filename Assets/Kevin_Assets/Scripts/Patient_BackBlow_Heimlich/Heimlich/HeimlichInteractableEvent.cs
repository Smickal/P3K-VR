using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class HeimlichInteractableEvent : MonoBehaviour
{
    [SerializeField]private HandGrabInteractable handGrab;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    [SerializeField]private GameObject leftGrabber, rightGrabber;

    [SerializeField]private GameObject currGrabber;
    [SerializeField] HeimlichMovement _heimlichMovement;

    public void OnGrabHT()
    {
        if(handGrab.HasSelectingInteractor(leftGrabberHT))currGrabber = leftGrabber;
        else if (handGrab.HasSelectingInteractor(rightGrabberHT))currGrabber = rightGrabber;
        // Debug.Log(handGrab.HasSelectingInteractor(leftGrabberHT) + "apakah iya");
        else return;
        // Debug.Log(currGrabber+"grabbernya sekarang");
        _heimlichMovement.SetGrabber(currGrabber);

        // Debug.Log(handGrab.HasSelectingInteractor(leftGrabberHT) + " and " + handGrab.HasSelectingInteractor(rightGrabberHT));
        // handGrab.Interactors
    }

    public void OnReleaseHT()
    {
        // base.OnRelease();
        if(PlayerRestriction.IsRestrictMovement())PlayerRestriction.LiftMovementRestriction();
        _heimlichMovement.ReleaseGrabber(currGrabber);
        currGrabber = null;
        
    }
}

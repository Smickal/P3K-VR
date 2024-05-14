using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class ChestPullInteractableEvent : MonoBehaviour
{
    [SerializeField]private HandGrabInteractable handGrab;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    [SerializeField]private GameObject leftGrabber, rightGrabber;

    [SerializeField]private GameObject currGrabber;
    [SerializeField] BackBlowMovement _bacBlowMov;

    public void OnGrabHT()
    {
        if(PlayerRestriction.IsRestrictMovement != null)
        {
            if(!PlayerRestriction.IsRestrictMovement())
            {
                if(PlayerRestriction.ApplyMovementRestriction != null)PlayerRestriction.ApplyMovementRestriction();
            }
        }
        if(handGrab.HasSelectingInteractor(leftGrabberHT))currGrabber = leftGrabber;
        else if (handGrab.HasSelectingInteractor(rightGrabberHT))currGrabber = rightGrabber;
        // Debug.Log(handGrab.HasSelectingInteractor(leftGrabberHT) + "apakah iya");
        else return;
        // Debug.Log(currGrabber+"grabbernya sekarang");
        _bacBlowMov.SetPullGrabber(currGrabber);

        // Debug.Log(handGrab.HasSelectingInteractor(leftGrabberHT) + " and " + handGrab.HasSelectingInteractor(rightGrabberHT));
        // handGrab.Interactors
    }

    public void OnReleaseHT()
    {
        // base.OnRelease();
        if(PlayerRestriction.IsRestrictMovement != null)
        {
            if(PlayerRestriction.IsRestrictMovement())
            {
                if(PlayerRestriction.LiftMovementRestriction != null)
                {
                    PlayerRestriction.LiftMovementRestriction();
                }
            }
        }
        currGrabber = null;
        _bacBlowMov.SetPullGrabber(currGrabber);
        

    }
}

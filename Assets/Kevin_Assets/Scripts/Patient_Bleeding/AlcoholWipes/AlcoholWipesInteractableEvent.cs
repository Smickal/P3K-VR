using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class AlcoholWipesInteractableEvent : MonoBehaviour
{
    [SerializeField]GameObject Lid, WipeOut, SnapPoint;
    [SerializeField]Collider Lid_Collider;
    [Header("Reference")]
    [SerializeField]private HandGrabInteractable handGrab;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    public void OnGrabHT()
    {
        Lid_Collider.enabled = false;
        WipeOut.SetActive(true);
        SnapPoint.SetActive(true);

        Lid.SetActive(false);
        if(handGrab.HasSelectingInteractor(leftGrabberHT))
        {
            StartCoroutine(GrabRelease(leftGrabberHT));
        }
        else if (handGrab.HasSelectingInteractor(rightGrabberHT))
        {
            StartCoroutine(GrabRelease(rightGrabberHT));
        }
    }
    IEnumerator GrabRelease(HandGrabInteractor grabber)
    {
        yield return new WaitForSeconds(.1f);
        grabber.ForceRelease();
    }
    
}

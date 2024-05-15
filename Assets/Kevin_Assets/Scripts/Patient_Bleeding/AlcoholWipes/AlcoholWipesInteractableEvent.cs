using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class AlcoholWipesInteractableEvent : MonoBehaviour
{
    [SerializeField]GameObject Lid, WipeOut, SnapPoint;
    [SerializeField]Collider Lid_Collider;
    [Header("Reference")]
    [SerializeField]private HandGrabInteractable[] handGrabs;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    private void Awake() {
        if(handGrabs == null)
        {
            handGrabs = GetComponentsInChildren<HandGrabInteractable>().ToArray();
        }
    }
    public void OnGrabHT()
    {
        
        HandGrabInteractor currHand = CheckHandGrabInteractor();
        if(currHand != null)
        {
            Lid_Collider.enabled = false;
            WipeOut.SetActive(true);
            SnapPoint.SetActive(true);
            // snapPoint_Collider.enabled = true;
            // alcoholWipeCollider.enabled = true;

            Lid.SetActive(false);
            // Debug.Log(currHand + " Hands ");
            StartCoroutine(GrabRelease(currHand));
        }
        // if(handGrab.HasSelectingInteractor(leftGrabberHT))
        // {
        //     StartCoroutine(GrabRelease(leftGrabberHT));
        // }
        // else if (handGrab.HasSelectingInteractor(rightGrabberHT))
        // {
        //     StartCoroutine(GrabRelease(rightGrabberHT));
        // }
    }
    IEnumerator GrabRelease(HandGrabInteractor grabber)
    {
        yield return new WaitForSeconds(.1f);
        grabber.ForceRelease();
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
    
}

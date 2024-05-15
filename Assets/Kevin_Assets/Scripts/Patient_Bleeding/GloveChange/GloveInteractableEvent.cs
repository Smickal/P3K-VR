using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class GloveInteractableEvent : MonoBehaviour
{
    [SerializeField] GloveChangeManager _manager;
    
    [Header("Reference")]
    [SerializeField]private HandGrabInteractable[] handGrabs;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    [Header("Pas di luar rumah, Freeze All Rb")]
    public bool DebugOnly;
    private void Awake() {
        if(handGrabs == null)
        {
            handGrabs = GetComponentsInChildren<HandGrabInteractable>().ToArray();
        }
    }
    public void OnGrabHT()
    {
        if(!DebugOnly)
        {
            if(GameManager.CheckLevelTypeNow == null || GameManager.CheckInGameModeNow == null|| BleedingWithoutEmbeddedItem.StateFirstAidNow == null)return;
            if((GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || 
            GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid || 
            BleedingWithoutEmbeddedItem.StateFirstAidNow() != BleedingWithoutEmbeddedItem_State.WearGloves))return;
        }

        
        // if(handGrab.HasSelectingInteractor(leftGrabberHT))
        // {
        //     StartCoroutine(GrabRelease(leftGrabberHT));
        // }
        // else if (handGrab.HasSelectingInteractor(rightGrabberHT))
        // {
        //     StartCoroutine(GrabRelease(rightGrabberHT));
        // }
        HandGrabInteractor currHand = CheckHandGrabInteractor();
        if(currHand != null)
        {
            if(_manager)_manager.ChangeToGlove();
            // Debug.Log(currHand + " Hands ");
            StartCoroutine(GrabRelease(currHand));
        }
    }
    IEnumerator GrabRelease(HandGrabInteractor grabber)
    {
        // Debug.Log("What");
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

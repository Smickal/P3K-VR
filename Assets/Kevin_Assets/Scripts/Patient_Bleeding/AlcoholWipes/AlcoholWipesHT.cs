using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Unity.VisualScripting;
using UnityEngine;

public class AlcoholWipesHT : MonoBehaviour
{
    [SerializeField]private HandGrabInteractable[] handGrabs;
    [SerializeField]private DistanceHandGrabInteractable[] distanceHandGrabs;
    [SerializeField]private HandGrabInteractor leftGrabberHT,rightGrabberHT;
    [SerializeField]private DistanceHandGrabInteractor leftDistanceGrabberHT, rightDistanceGrabberHT;
    [SerializeField]private GameObject _leftGrabberHT, _rightGrabberHT;
    GameObject currGrabber;

    AlcoholCleanManager alcoholManager;
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

    private void Start()
    {
        alcoholManager = AlcoholCleanManager.Instance;
    }

    public void OnGrabHT()
    {
        HandGrabInteractor currHand = CheckHandGrabInteractor();
        DistanceHandGrabInteractor currDistanceHand = CheckDistanceHandGrabInteractor();
        
        if(currHand == leftGrabberHT || currDistanceHand == leftDistanceGrabberHT)
        {
            // Debug.Log("Left Hand");
            currGrabber = _leftGrabberHT;
        }
        else if (currHand == rightGrabberHT || currDistanceHand == rightDistanceGrabberHT)
        {
            // Debug.Log("Right Hand");
            currGrabber = _rightGrabberHT;
        }
        else
        {
            currGrabber = null;
            return;
        }
        
        alcoholManager?.RegisterGrabber(currGrabber);
        alcoholManager.IsHolding = true;
    }

    public void OnReleaseHT()
    {
        if(currGrabber == null)return;
        currGrabber = null;
        alcoholManager.IsHolding = false;
        alcoholManager.SaveCurrentTimeProgress();
    }
    private HandGrabInteractor CheckHandGrabInteractor()
    {
        foreach(HandGrabInteractable handGrabInteractable in handGrabs)
        {
            // Debug.Log("handgrab" + handGrabInteractable);
            if(handGrabInteractable.HasSelectingInteractor(leftGrabberHT))
            {
                // Debug.Log(handGrabInteractable + " This is the chosen onee left" + leftGrabberHT);
                return leftGrabberHT;
            }
            else if (handGrabInteractable.HasSelectingInteractor(rightGrabberHT))
            {
                // Debug.Log(handGrabInteractable + " This is the chosen onee right" + rightGrabberHT);
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


}

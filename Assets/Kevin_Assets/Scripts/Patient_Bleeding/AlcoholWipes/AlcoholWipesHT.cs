using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class AlcoholWipesHT : MonoBehaviour
{
    [SerializeField]private HandGrabInteractable handGrab;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    [SerializeField]private GameObject _leftGrabberHT, _rightGrabberHT;
    GameObject currGrabber;

    AlcoholCleanManager alcoholManager;

    private void Start()
    {
        alcoholManager = AlcoholCleanManager.Instance;
    }

    public void OnGrabHT()
    {
        if(handGrab.HasSelectingInteractor(leftGrabberHT))
        {
            currGrabber = _leftGrabberHT;
        }
        else if (handGrab.HasSelectingInteractor(rightGrabberHT))
        {
            currGrabber = _rightGrabberHT;
        }
        else
        {
            currGrabber = null;
            // Debug.Log("Pasti Bakal Lewat Sini krn snap");
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

}

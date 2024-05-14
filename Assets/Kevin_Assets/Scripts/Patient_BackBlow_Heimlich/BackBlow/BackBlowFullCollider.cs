using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class BackBlowFullCollider : MonoBehaviour
{
    [SerializeField]bool hitFull;
    public bool HitFull { get { return hitFull; }set { hitFull = value; } }
    // [SerializeField] BackBlowMovement backBlowMovement;
    [SerializeField] Collider _leftGrabber;
    [SerializeField] Collider _rightGrabber;
    [SerializeField] Collider _leftGrabberFull;
    [SerializeField] Collider _rightGrabberFull;

    [SerializeField] Collider _leftGrabberHT;
    [SerializeField] Collider _rightGrabberHT;
    [SerializeField] Collider _leftGrabberHTFull;
    [SerializeField] Collider _rightGrabberHTFull;
    Collider firstColliderWhoTrigger;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Yang Collider adalah Atas" + other.gameObject);
        if(InteractToolsController.CheckIsHandTrackOn == null)return;
        if(!InteractToolsController.CheckIsHandTrackOn()) if(!(other == _leftGrabber || other == _rightGrabber || other == _leftGrabberFull || other == _rightGrabberFull)) return;
        else if(InteractToolsController.CheckIsHandTrackOn()) if(!(other == _leftGrabberHT || other == _rightGrabberHT || other == _leftGrabberHTFull || other == _rightGrabberHTFull)) return;

        // Debug.Log("Yang Collider adalah" + other.gameObject);
        firstColliderWhoTrigger = other;
        hitFull = true;

    }


    private void OnTriggerExit(Collider other)
    {
        if(InteractToolsController.CheckIsHandTrackOn == null)return;
        if(!InteractToolsController.CheckIsHandTrackOn()) if(!(other == _leftGrabber || other == _rightGrabber || other == _leftGrabberFull || other == _rightGrabberFull)) return;
        else if(InteractToolsController.CheckIsHandTrackOn()) if(!(other == _leftGrabberHT || other == _rightGrabberHT || other == _leftGrabberHTFull || other == _rightGrabberHTFull)) return;
        // if(other != firstColliderWhoTrigger)return;
        // hitFull = false;
        firstColliderWhoTrigger = null;
    }
}

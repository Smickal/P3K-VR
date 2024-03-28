using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class BackBlowFullCollider : MonoBehaviour
{
    [SerializeField]bool hitFull;
    public bool HitFull { get { return hitFull; } }
    [SerializeField] BackBlowMovement backBlowMovement;
    [SerializeField] Grabber _leftGrabber;
    [SerializeField] Grabber _rightGrabber;
    [SerializeField] GameObject _leftGrabberFull;
    [SerializeField] GameObject _rightGrabberFull;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if(!(other.gameObject == _leftGrabber.gameObject || other.gameObject == _rightGrabber.gameObject || other.gameObject == _leftGrabberFull || other.gameObject == _rightGrabberFull)) return;
        // if(backBlowMovement.EnterNormalCollider)
        // {
        //     hitFull = false;
        //     return;
        // }
        hitFull = true;

    }


    private void OnTriggerExit(Collider other)
    {
        if(!(other.gameObject == _leftGrabber.gameObject || other.gameObject == _rightGrabber.gameObject || other.gameObject == _leftGrabberFull || other.gameObject == _rightGrabberFull)) return;
        hitFull = false;
    }
}

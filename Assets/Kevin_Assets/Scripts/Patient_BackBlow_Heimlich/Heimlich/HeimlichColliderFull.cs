using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class HeimlichColliderFull : MonoBehaviour
{
    [SerializeField]bool hitFull;
    public bool HitFull { get { return hitFull; } }
    [SerializeField] Grabber _leftGrabber;
    [SerializeField] Grabber _rightGrabber;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if(!(other.gameObject == _leftGrabber.gameObject || other.gameObject == _rightGrabber.gameObject)) return;
        hitFull = true;

    }


    private void OnTriggerExit(Collider other)
    {
        if(!(other.gameObject == _leftGrabber.gameObject || other.gameObject == _rightGrabber.gameObject)) return;
        hitFull = false;
    }
}

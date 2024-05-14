using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class HeimlichColliderFull : MonoBehaviour
{
    [SerializeField]bool hitFull;
    public bool HitFull { get { return hitFull; } }
    [SerializeField] Grabbable _leftGrabbable;
    [SerializeField] Grabbable _rightGrabbable;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject);
        if(!(other.gameObject == _leftGrabbable.gameObject || other.gameObject == _rightGrabbable.gameObject)) return;
        hitFull = true;

    }


    private void OnTriggerExit(Collider other)
    {
        if(!(other.gameObject == _leftGrabbable.gameObject || other.gameObject == _rightGrabbable.gameObject)) return;
        hitFull = false;
    }
}

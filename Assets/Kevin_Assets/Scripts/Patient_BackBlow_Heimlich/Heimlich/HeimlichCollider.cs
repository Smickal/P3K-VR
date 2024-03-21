using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeimlichCollider : MonoBehaviour
{
    [SerializeField] HeimlichMovement _heimlichMov;

    bool isLeftHit;
    bool isRightHit;
    private void OnTriggerEnter(Collider other)
    {


        //check Left and Right Grabbable Collider
        //Check TargetCollider
        //Need to check 3 of them to be considered true

        _heimlichMov.CheckForGrababbleTrig(other);
        _heimlichMov.CheckForTargetTrigger(other);


    }


    private void OnTriggerExit(Collider other)
    {
        _heimlichMov.CheckForGrabbableExit(other);
    }
}

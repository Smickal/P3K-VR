using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeimlichCollider : MonoBehaviour
{
    [SerializeField] bool _isFullScore = true;
    [SerializeField] HeimlichMovement _heimlichMov;

    private void OnTriggerEnter(Collider other)
    {


        //check Left and Right Grabbable Collider
        //Check TargetCollider
        //Need to check 3 of them to be considered true

        //TRIGGER CHECK FOR BOTH FINGERS
        _heimlichMov.CheckForGrababbleTrig(other);

        //TRIGGER CHECK FOR TARGET
        //TRIGGER CHECK FOR SCORE
        _heimlichMov.CheckForTargetTrigger(other, _isFullScore);


    }


    private void OnTriggerExit(Collider other)
    {
        _heimlichMov.CheckForGrabbableExit(other);
    }
}

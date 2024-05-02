using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBriefcaseButtonPress : MonoBehaviour
{
    [SerializeField] GameObject _handTrackLeft, handTrackRight;
    [SerializeField] Briefcase _briefCase;

    private void OnTriggerEnter(Collider other)
    {
        Grabber grabber = other.GetComponent<Grabber>();

        if(grabber != null || other.gameObject.layer == 15 || other.gameObject == _handTrackLeft || other.gameObject == handTrackRight) 
        {
            Debug.Log(other.gameObject + "yang buka"); 
            // Debug.Log("Grabber " + grabber != null + "Layer" + other.gameObject.layer + " handtrack?" + )
            _briefCase.TriggerOpenCloseAnim();
        }
    }
}

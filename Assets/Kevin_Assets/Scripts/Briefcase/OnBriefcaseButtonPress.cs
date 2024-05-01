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

        if(grabber != null || other.gameObject.layer == 15 || other.gameObject != _handTrackLeft || other.gameObject != handTrackRight) 
        {
            _briefCase.TriggerOpenCloseAnim();
        }
    }
}

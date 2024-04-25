using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBriefcaseButtonPress : MonoBehaviour
{
    [SerializeField] Briefcase _briefCase;

    private void OnTriggerEnter(Collider other)
    {
        Grabber grabber = other.GetComponent<Grabber>();

        if(grabber != null)
        {
            _briefCase.TriggerOpenCloseAnim();
        }
    }
}

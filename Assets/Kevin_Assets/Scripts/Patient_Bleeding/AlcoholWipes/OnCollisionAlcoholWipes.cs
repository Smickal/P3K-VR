using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionAlcoholWipes : MonoBehaviour
{
    AlcoholCleanManager cleanManager;

    private void Start()
    {
        cleanManager = AlcoholCleanManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Grabber grabber = collision.gameObject.GetComponent<Grabber>();


        if (grabber == null) return;
        cleanManager.RegisterCurrentNeedToCleanGrabber(grabber);
    }

    private void OnTriggerEnter(Collider other)
    {
        Grabber grabber = other.gameObject.GetComponent<Grabber>();


        if (grabber == null) return;
        cleanManager.RegisterCurrentNeedToCleanGrabber(grabber);
    }
}

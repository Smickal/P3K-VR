using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionButton : MonoBehaviour
{
    [SerializeField] UnityEvent _onButtonCollisionEvent;
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Grabber>() == null) return;
        _onButtonCollisionEvent.Invoke();


    }
}

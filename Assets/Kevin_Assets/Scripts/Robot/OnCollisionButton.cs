using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionButton : MonoBehaviour
{
    [SerializeField] GameObject _handTrackLeft, handTrackRight;
    [SerializeField] UnityEvent _onButtonCollisionEvent;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Yang masuk sini" + other.gameObject);
        if (other.GetComponent<Grabber>() == null && other.gameObject.layer != 15 && other.gameObject != _handTrackLeft && other.gameObject != handTrackRight) return;
        _onButtonCollisionEvent.Invoke();


    }
}

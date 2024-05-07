using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionButton : MonoBehaviour
{
    [SerializeField] GameObject _handTrackLeft, handTrackRight;
    [SerializeField] UnityEvent _onButtonCollisionEvent;
    public AudioClip SoundOnTriggerButton;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Yang masuk sini" + other.gameObject);
        if (other.GetComponent<Grabber>() == null && other.gameObject.layer != 15 && other.gameObject != _handTrackLeft && other.gameObject != handTrackRight) return;
        
        PlaySound();
        _onButtonCollisionEvent.Invoke();
        
    }
    private void PlaySound()
    {
        if (SoundOnTriggerButton) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                VRUtils.Instance.PlaySpatialClipAt(SoundOnTriggerButton, transform.position, 0.75f);
            }
        }
    }
}

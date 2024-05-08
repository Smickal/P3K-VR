using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.Events;

public class PlayerTriggerEvents : MonoBehaviour
{
    public UnityEvent OnTriggerCollision;
    public AudioClip SoundOnEnter;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Wat???");
            PlaySound();
            OnTriggerCollision.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlaySound();
            OnTriggerCollision.Invoke();
        }
    }
    private void PlaySound()
    {
        if (SoundOnEnter) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                VRUtils.Instance.PlaySpatialClipAt(SoundOnEnter, transform.position, 0.75f);
            }
        }
    }
}

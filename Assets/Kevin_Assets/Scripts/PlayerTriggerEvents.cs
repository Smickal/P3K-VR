using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.Events;

public class PlayerTriggerEvents : MonoBehaviour
{
    public UnityEvent OnTriggerCollision, OnTriggerExitCollision;
    public AudioClip SoundOnEnter, SoundOnExit;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Wat???");
            PlaySoundEnter();
            OnTriggerCollision.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlaySoundEnter();
            OnTriggerCollision.Invoke();
        }
    }
    private void PlaySoundEnter()
    {
        if (SoundOnEnter) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                VRUtils.Instance.PlaySpatialClipAt(SoundOnEnter, transform.position, 0.75f);
            }
        }
    }
    private void PlaySoundExit()
    {
        if (SoundOnExit) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                VRUtils.Instance.PlaySpatialClipAt(SoundOnExit, transform.position, 0.75f);
            }
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Wat???");
            PlaySoundExit();
            OnTriggerExitCollision?.Invoke();
        }
    }
    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlaySoundExit();
            OnTriggerExitCollision?.Invoke();
        }
    }
}

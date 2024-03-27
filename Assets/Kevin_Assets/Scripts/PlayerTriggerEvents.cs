using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerEvents : MonoBehaviour
{
    public UnityEvent OnTriggerCollision;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OnTriggerCollision.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Haloo");
            OnTriggerCollision.Invoke();
        }
    }
}

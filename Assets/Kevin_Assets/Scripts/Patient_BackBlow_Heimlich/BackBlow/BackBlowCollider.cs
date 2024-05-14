using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackBlowCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject);
    
        BackBlowMovement.OnBackBlow.Invoke(other);

    }


    private void OnTriggerExit(Collider other)
    {
        BackBlowMovement.OnReleaseBackBlow.Invoke(other);
    }
}


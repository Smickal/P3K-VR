using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBlowCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        BackBlowMovement.OnBackBlow.Invoke(other);

    }


    private void OnTriggerExit(Collider other)
    {
        BackBlowMovement.OnReleaseBackBlow.Invoke(other);
    }
}


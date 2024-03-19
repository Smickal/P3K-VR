using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBlowCollider : MonoBehaviour
{
    [SerializeField] BackBlowMovement _backBlow;
    private void OnTriggerEnter(Collider other)
    {

        BackBlowMovement.OnBackBlow.Invoke(other);

    }

}


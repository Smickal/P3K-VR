using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageMovementColliderEvent : MonoBehaviour
{
    [SerializeField] BandageMovement _bandageMovement;

    private void OnTriggerEnter(Collider other)
    {
        Bandage hitBandage = other.GetComponent<Bandage>();
        if(hitBandage != null )
        {
            _bandageMovement.RegisterBandage(hitBandage);
        }
    }


}

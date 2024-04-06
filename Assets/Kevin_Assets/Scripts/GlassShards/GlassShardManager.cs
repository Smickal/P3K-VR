using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShardManager : MonoBehaviour
{
    [SerializeField] SnapZone _bandageSnapZone;
    [SerializeField] BandageMovement _bandageMovement;



    bool isDoneBandageMovement = false;
    bool isBandageSnappedToHold = false;

    private void Start()
    {
        _bandageMovement.OnMovementDone += DoneBandageMovement;
        _bandageMovement.gameObject.SetActive(false);
    }

    private void DoneBandageMovement()
    {
        isDoneBandageMovement = true;

        //Trigger DOne Movement Bandage Here
    }


    private void Update()
    {
        if(_bandageSnapZone.HeldItem != null && isBandageSnappedToHold == false)
        {
            isBandageSnappedToHold = true;
            _bandageMovement.gameObject.SetActive(true);
            _bandageSnapZone.CanRemoveItem = false;
        }

    }
}

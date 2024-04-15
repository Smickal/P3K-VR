using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShardManager : MonoBehaviour
{
    [SerializeField] SnapZone[] _bandageSnapZones;
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
        if(AllSnapZoneHeldItem() && isBandageSnappedToHold == false)
        {
            isBandageSnappedToHold = true;
            _bandageMovement.gameObject.SetActive(true);
            SetSnapZone_CantRemoveItem();
        }

    }

    public bool AllSnapZoneHeldItem()
    {
        if(_bandageSnapZones == null)return false;
        foreach(SnapZone _bandageSnapZone in _bandageSnapZones)
        {
            if(_bandageSnapZone.HeldItem == null)
            {
                return false;
            }
        }
        return true;
    }
    public void SetSnapZone_CantRemoveItem()
    {
        if(_bandageSnapZones == null) return;
        foreach(SnapZone _bandageSnapZone in _bandageSnapZones)
        {
            _bandageSnapZone.CanRemoveItem = false;
        }
    }
}

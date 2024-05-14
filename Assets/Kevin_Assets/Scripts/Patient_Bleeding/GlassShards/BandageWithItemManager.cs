using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BandageWithItemManager : MonoBehaviour
{
    [SerializeField] SnapZone[] _bandageSnapZones;
    [SerializeField] GameObject[] snapZonesRings;
    [SerializeField] BandageMovement _bandageMovement;
    [SerializeField] private GameObject circleMovement;

    [SerializeField]Patient_Bleeding patient_Bleeding;
    [SerializeField]PatientBleedingQuestUI patientBleedingQuestUI;
    [SerializeField]DialogueManager dialogueManager;

    bool isDoneBandageMovement = false;
    public bool IsDoneBandageMovement{get{return isDoneBandageMovement;}}
    bool isBandageSnappedToHold = false;
    
    
    public UnityEvent OnBandageMovementDone;
    private void Start()
    {
        _bandageMovement.OnMovementDone += DoneBandageMovement;
        _bandageMovement.gameObject.SetActive(false);
    }

    private void DoneBandageMovement()
    {
        isDoneBandageMovement = true;

        OnBandageMovementDone.Invoke();
    }


    private void Update()
    {
        if(AllSnapZoneHeldItem() && isBandageSnappedToHold == false)
        {
            if(patient_Bleeding.BleedingQuest_State == BleedingQuest_State.WithItem)patientBleedingQuestUI.ActivateToolTipWithItem(2);
            
            dialogueManager.PlayDialogueScene(DialogueListTypeParent.Bleeding_Explanation, DialogueListType_Bleeding_Explanation.Bleeding_NextStep);

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
            _bandageSnapZone.CanRemoveChange(false);
        }
        foreach(GameObject snapzonering in snapZonesRings)
        {
            snapzonering.SetActive(false);
        }
    }

    public void ActivateBandageWithItem()
    {
        
        foreach(SnapZone _bandageSnapZone in _bandageSnapZones)
        {
            _bandageSnapZone.gameObject.SetActive(true);
        }
        circleMovement.SetActive(true);
    }
    public void DeactivateBandageWithItem()
    {
        foreach(SnapZone _bandageSnapZone in _bandageSnapZones)
        {
            _bandageSnapZone.gameObject.SetActive(false);
        }
        circleMovement.SetActive(false);
    }
    public void DeactivateBandageWithItem_WithoutSnapZone()
    {
        circleMovement.SetActive(false);
    }
}

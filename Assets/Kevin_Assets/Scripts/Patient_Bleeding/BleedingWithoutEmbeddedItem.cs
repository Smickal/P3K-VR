using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BleedingWithoutEmbeddedItem_State
{
    CleanHands, WearGloves, StopBleed, CleanBlood, DryWater, BandageTime, PuttingLegOnTopSomethingTime, Done, None
}
public class BleedingWithoutEmbeddedItem : MonoBehaviour, ITurnOffStatic
{
    
    [SerializeField]private Patient_Bleeding patient_Bleeding;
    [SerializeField]private BleedingWithoutEmbeddedItem_State state;    
    public BleedingWithoutEmbeddedItem_State BleedingWithoutEmbeddedItem_State{get{return state;}}
    IEnumerator FirstAidCoroutine;
    private bool isDoneFirstAid;
    public bool IsDoneFirstAid{get{return isDoneFirstAid;}}
    [Header("CleanHands")]
    [SerializeField]AlcoholCleanManager alcoholCleanManager;
    [Header("WearGloves")]
    [SerializeField]GloveChangeManager glovesChangeManager;
    [Header("StopBleed")]
    [SerializeField]Collider _bleedingColl;
    [SerializeField]DirtyObject _bleedingObj;
    [Header("CleanBlood")]
    [SerializeField]Collider _cleanColl;
    [SerializeField]DirtyObject _cleanObj;
    [Header("DryWater")]
    [SerializeField]Collider _dryColl;
    [SerializeField]DirtyObject _dryObj;
    [Header("BandageTime")]
    [SerializeField]BandageWithItemManager bandageTime;
    [Header("PuttingLegOnTopSomethingTime")]
    [SerializeField]GameObject Leg;
    [SerializeField]LegMoveManager leftLeg, rightLeg;
    //put collider dr snapzone yg hrs di grab
    public bool isPuttingLegDone;

    public static Func<BleedingWithoutEmbeddedItem_State> StateFirstAidNow;

    private void Awake() 
    {
        StateFirstAidNow += StateNow;
        
    }
    private void Start()
    {
        _bleedingColl.enabled = false;
        _cleanColl.enabled = false;
        _dryColl.enabled = false;
        Leg.SetActive(false);
        bandageTime.DeactivateBandageWithItem();
    }

    public BleedingWithoutEmbeddedItem_State StateNow(){return state;}
    
    public void ActivateFirstAid()
    {
        state = BleedingWithoutEmbeddedItem_State.CleanHands;
        Leg.SetActive(false);
        _bleedingColl.enabled = false;
        _cleanColl.enabled = false;
        _dryColl.enabled = false;
        bandageTime.DeactivateBandageWithItem();
        //matiin collider utk leg

        FirstAidCoroutine = BleedingWithoutItem();
        StartCoroutine(FirstAidCoroutine);
    }
    public void DeactivateFirstAid()
    {
        _bleedingColl.enabled = false;
        _cleanColl.enabled = false;
        _dryColl.enabled = false;
        bandageTime.DeactivateBandageWithItem();
        if(FirstAidCoroutine != null)StopCoroutine(FirstAidCoroutine);
    }
    private IEnumerator BleedingWithoutItem()
    {
        yield return new WaitUntil(()=> alcoholCleanManager.IsDoneCleaning);
        state = BleedingWithoutEmbeddedItem_State.WearGloves;

        yield return new WaitUntil(()=> glovesChangeManager.IsDoneChanging);
        state = BleedingWithoutEmbeddedItem_State.StopBleed;
        _bleedingColl.enabled = true;

        yield return new WaitUntil(()=> _bleedingObj.IsDoneCleaning);
        state = BleedingWithoutEmbeddedItem_State.CleanBlood;
        _bleedingColl.enabled = false;
        _cleanColl.enabled = true;

        yield return new WaitUntil(()=> _cleanObj.IsDoneCleaning);
        state = BleedingWithoutEmbeddedItem_State.DryWater;
        _cleanColl.enabled = false;
        _dryColl.enabled = true;

        yield return new WaitUntil(()=> _dryObj.IsDoneCleaning);
        state = BleedingWithoutEmbeddedItem_State.BandageTime;
        _dryColl.enabled = false;
        bandageTime.ActivateBandageWithItem();

        yield return new WaitUntil(()=> bandageTime.IsDoneBandageMovement);
        state = BleedingWithoutEmbeddedItem_State.PuttingLegOnTopSomethingTime;
        bandageTime.DeactivateBandageWithItem();
        Leg.SetActive(true);
        //nyalakan collider leg

        yield return new WaitUntil(()=> leftLeg.IsMovementDone && rightLeg.IsMovementDone);
        state = BleedingWithoutEmbeddedItem_State.Done;
        //matikan collider leg
        IsDone();
        


        FirstAidCoroutine = null;
    }

    public void IsDone()
    {
        isDoneFirstAid = true;

        patient_Bleeding.FirstAid_BleedingWithoutItemDone();
    }
    public void TurnOffStatic()
    {
        StateFirstAidNow -= StateNow;
    }
    public bool IsLegDone()
    {
        return false;
    }
}

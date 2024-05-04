using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class Briefcase : MonoBehaviour
{
    // Start is called before the first frame update
    int OpenBriefHash = Animator.StringToHash("OpenCase");
    int CloseBriefHash = Animator.StringToHash("CloseCase");

    [Header("Reference")]
    [SerializeField] Animator _briefCaseAnim;
    [SerializeField] Collider[] _buttonColliders;
    //[SerializeField] BoxCollider _boxCollider;

    [SerializeField] Collider[] _inventoryColliders;
    [SerializeField] SnapZone[] _snapZones;

    bool isOpen = false, isInventEnabled = true;
    public bool IsOpen { get { return isOpen; } }


    private void Update() 
    {
        if(isOpen)
        {
            if(!PlayerRestriction.IsRestrictGrabable()) EnableInventory();
            else DisableInventory();
        }
        else if(!isOpen)
        {
            DisableInventory();
        }
    }
    public void TriggerOpenCloseAnim()
    {
        
        if (isOpen == false)
        {
            _briefCaseAnim.SetTrigger(OpenBriefHash);
            EnableInventory();
            isOpen = true;
        }
        else
        {
            _briefCaseAnim.SetTrigger(CloseBriefHash);
            DisableInventory();
            isOpen = false;
        }
    }

    public void DisableInventory()
    {
        // if(!isInventEnabled) return;
        if(!isInventEnabled)isInventEnabled = false;
        foreach(Collider _inventoryCollider in _inventoryColliders)
        {
            if(!_inventoryCollider.enabled)continue;

            _inventoryCollider.enabled = false;
        }
        foreach(SnapZone snapzone in _snapZones)
        {
            if(snapzone.HeldItem != null)
            {
                GameObject SnapInteractor = snapzone.HeldItem.transform.Find("SnapInteractor").gameObject;
                if(SnapInteractor != null)
                {
                    SnapInteractor.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
    public void EnableInventory()
    {
        // if(isInventEnabled) return;
        if(isInventEnabled)isInventEnabled = true;
        foreach(Collider _inventoryCollider in _inventoryColliders)
        {
            if(_inventoryCollider.enabled)continue;

            _inventoryCollider.enabled = true;
        }
        foreach(SnapZone snapzone in _snapZones)
        {
            if(snapzone.HeldItem != null)
            {
                GameObject SnapInteractor = snapzone.HeldItem.transform.Find("SnapInteractor").gameObject;
                if(SnapInteractor != null)
                {
                    SnapInteractor.GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

    public void SnapToSnapZone()
    {
        foreach(Collider buttonColl in _buttonColliders)
        {
            buttonColl.enabled = false;
        }
        if(isOpen)
        {
            TriggerOpenCloseAnim();
            DisableInventory();
        }
        
    }
    public void UnSnap()
    {
        foreach(Collider buttonColl in _buttonColliders)
        {
            buttonColl.enabled = true;
        }
    }
}

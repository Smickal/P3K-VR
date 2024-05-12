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
    public AudioClip SoundOnTriggerButton;

    bool isOpen = false, isInventEnabled = true, isAnimPlay = false;
    public bool IsOpen { get { return isOpen; } }
    public bool canOnlyOpenInCertainSpot;

    private void Start() {
        if(canOnlyOpenInCertainSpot)
        {
            ChangeButtonCollEnable(false);
        }
    }   
    private void Update() 
    {
        if(canOnlyOpenInCertainSpot)
        {
            ChangeButtonCollEnable(false);
        }
        if(isOpen)
        {
            if((PlayerRestriction.IsRestrictGrabable != null) && !PlayerRestriction.IsRestrictGrabable()) EnableInventory();
            else DisableInventory();
        }
        else if(!isOpen)
        {
            DisableInventory();
        }
    }
    public void TriggerOpenCloseAnim()
    {
        if(isAnimPlay)return;
        PlaySound();
        if (isOpen == false)
        {
            _briefCaseAnim.SetTrigger(OpenBriefHash);
            EnableInventory();
            isOpen = true;
            isAnimPlay = true;
        }
        else
        {
            _briefCaseAnim.SetTrigger(CloseBriefHash);
            DisableInventory();
            isOpen = false;
            isAnimPlay = true;
        }
    }
    public void AnimationFinish()
    {
        isAnimPlay = false;
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
        ChangeButtonCollEnable(false);
        if(isOpen)
        {
            _briefCaseAnim.SetTrigger(CloseBriefHash);
            DisableInventory();
            isOpen = false;
            isAnimPlay = true;

        }
        
    }
    public void UnSnap()
    {
        if(canOnlyOpenInCertainSpot)
        {
            Debug.Log("here");
            ChangeButtonCollEnable(false);
            return;
        }
        Debug.Log("Kan ga bisa lwt sini");
        ChangeButtonCollEnable(true);
    }
    private void PlaySound()
    {
        if (SoundOnTriggerButton) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                VRUtils.Instance.PlaySpatialClipAt(SoundOnTriggerButton, transform.position, 0.75f);
            }
        }
    }
    public void ChangeButtonCollEnable(bool change)
    {
        foreach(Collider buttonColl in _buttonColliders)
        {
            buttonColl.enabled = change;
        }
    }
    public void ChangeButtonCollEnableOnPlace(bool change)
    {
        canOnlyOpenInCertainSpot = false;
        foreach(Collider buttonColl in _buttonColliders)
        {
            buttonColl.enabled = change;
        }
    }
}

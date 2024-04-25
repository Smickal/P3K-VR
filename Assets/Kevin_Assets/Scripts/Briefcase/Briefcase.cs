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
    [SerializeField] Button _briefCaseBtn_Controller;
    [SerializeField] InteractableUnityEventWrapper _briefCaseBtn_HandTrack;
    //[SerializeField] BoxCollider _boxCollider;

    [SerializeField] Collider[] _inventoryColliders;

    bool isOpen = false, isInventEnabled;
    public bool IsOpen { get { return isOpen; } }

    private void Start()
    {
        _briefCaseBtn_Controller?.onButtonDown.AddListener(TriggerOpenCloseAnim);
        _briefCaseBtn_HandTrack?.WhenSelect.AddListener(TriggerOpenCloseAnim);

    }

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
        if(isInventEnabled)isInventEnabled = false;
        foreach(Collider _inventoryCollider in _inventoryColliders)
        {
            _inventoryCollider.enabled = false;
        }
    }
    public void EnableInventory()
    {
        if(!isInventEnabled)isInventEnabled = true;
        foreach(Collider _inventoryCollider in _inventoryColliders)
        {
            _inventoryCollider.enabled = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] Collider[] InventoryCollider;
    [SerializeField] SnapZone[] snapzones;
    [SerializeField] GameManager gameManager;
    private bool isUnlock = true;
    public bool debugOpen;
    public void ChangeEnable()
    {
        if(debugOpen)return;
        if(gameManager.LevelModeNow() == LevelMode.Level)
        {
            if(gameManager.InGame_ModeNow() == InGame_Mode.NormalWalk)
            {
                InventoryEnable(false);
            }
            else if(gameManager.InGame_ModeNow() == InGame_Mode.FirstAid)
            {
                if(gameManager.LevelTypeNow() == LevelP3KType.Bleeding)
                {
                    InventoryEnable(true);
                }
                else
                {
                    InventoryEnable(false);
                }
                
            }
        }
        else
        {
            InventoryEnable(true);
        }
    }
    public void InventoryEnable(bool change)
    {
        isUnlock = change;
        foreach(Collider coll in InventoryCollider)
        {
            coll.enabled = change;
        }
        foreach(SnapZone snapzone in snapzones)
        {
            if(snapzone.HeldItem == null)continue;
            GameObject snapInteractor = snapzone.HeldItem.transform.Find("SnapInteractor").gameObject;
            if(snapInteractor != null)
            {
                snapInteractor.GetComponent<Collider>().enabled = change;
            }
        }
    }
    public void CheckSnapZoneHeldItem(SnapZone snapZone)
    {
        if(snapZone.HeldItem == null)return;
        GameObject snapInteractor = snapZone.HeldItem.transform.Find("SnapInteractor").gameObject;
        if(snapInteractor != null)
        {
            snapInteractor.GetComponent<Collider>().enabled = isUnlock;
        }
    }
}

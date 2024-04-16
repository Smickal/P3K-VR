using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient_Bleeding : MonoBehaviour
{
    bool isDoneFirstAid;
    public bool IsDoneFirstAid{get{return isDoneFirstAid;}}
    
    [Header("Reference")]
    [SerializeField]private QuestManagerUI questManagerUI;
    [SerializeField]BandageWithItemManager BleedWithItemMgr;
    [SerializeField]BleedingWithoutEmbeddedItem bleedingWithoutEmbeddedItem;

    private void Start() 
    {
        ActivateBleedWithoutItem();
    }

    public void ActivateBleedWithoutItem()
    {
        bleedingWithoutEmbeddedItem.ActivateFirstAid();
        BleedWithItemMgr.DeactivateBandageWithItem();
    }
    public void ActivateGlassShard()
    {
        BleedWithItemMgr.ActivateBandageWithItem();
    }

    public void FirstAid_BleedingWithItemDone()
    {
        BleedWithItemMgr.DeactivateBandageWithItem();
        isDoneFirstAid = true;
    }
    public void FirstAid_BleedingWithoutItemDone()
    {
        questManagerUI.CloseHelper_Bleeding_WithoutItem();
        questManagerUI.OpenHelper_Bleeding_WithItem();
        
        ActivateGlassShard();
    }
    
    public void StopCoroutines()
    {
        bleedingWithoutEmbeddedItem.DeactivateFirstAid();
        BleedWithItemMgr.DeactivateBandageWithItem();
    }

}
